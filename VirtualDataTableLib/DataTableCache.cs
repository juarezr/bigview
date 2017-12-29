
// Based on: https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/virtual-mode-with-just-in-time-data-loading-in-the-datagrid


using System;
using System.Collections.Generic;
using System.Data;

namespace VirtualDataTableLib
{
    public class DataTableCache : IDisposable
    {
        #region Init

        private const int cachePageSize = 5;
        private List<DataPage> dataPagesCache;

        private int? totalRowCount = null;
        private int? totalPageCount = null;
        private int maxCachedRowIndex = 0;

        private int RowsPerPage;

        private int cachedPageLowBoundary = 0;
        private int cachedPageHighBoundary { get { return cachedPageLowBoundary + cachePageSize - 1; } }

        private IDataPageRetriever dataRetriever;

        public DataTableCache(IDataPageRetriever dataSupplier, int rowsPerPage)
        {
            dataRetriever = dataSupplier;
            RowsPerPage = rowsPerPage;
        }

        public static DataTableCache GetCacheFor(string sourceAddress, int rowsPerPage)
        {
            bool isParquet = sourceAddress
                .ToLowerInvariant()
                .EndsWith("parquet", System.StringComparison.Ordinal);

            IDataPageRetriever retriever;
            if (isParquet)
                retriever = new ParquetRetriever();
            else
                retriever = new AvroRetriever();

            retriever.OpenDataSource(sourceAddress);

            var cache = new DataTableCache(retriever, rowsPerPage);

            cache.LoadFirstPages();

            return cache;
        }

        public void Dispose()
        {
            if (dataRetriever != null)
            {
                dataRetriever.Dispose();
                dataRetriever = null;
            }
        }

        #endregion Init

        #region Properties

        public DataColumnCollection GetFields()
        {
            DataPage cachedPage = dataPagesCache[0];
            var cols = cachedPage.table.Columns;
            return cols;
        }

        public int? GetTotalRowCount()
        {
            return totalRowCount;
        }

        public int GetMaxLoadedRowIndex()
        {
            return maxCachedRowIndex;
        }

        public IDictionary<string, string> GetProperties()
        {
            return dataRetriever.GetProperties();
        }

        #endregion Properties

        #region Public Methods

        public object RetrieveElement(int rowIndex, int columnIndex)
        {
            object element;

            if (rowIndex == lastRowIndex)
                element = lastRow[columnIndex];
            else
                element = RetrieveElementByRowIndex(rowIndex, columnIndex);

            return element;
        }

        private object RetrieveElementByRowIndex(int rowIndex, int columnIndex)
        {
            int pageNumber = MapToPageNumber(RowsPerPage, rowIndex);

            PreloadPageToCache(pageNumber);

            object element = null;

            bool isCached = IsPageNumberInPageCache(pageNumber);
            if (isCached)
            {
                int cacheNumber = MapToCacheNumber(pageNumber);
                element = RetrieveElementFromPageCache(cacheNumber, rowIndex, columnIndex);
            }

            return element;
        }

        #endregion Public Methods

        #region Retrieve and Cache

        private void PreloadPageToCache(int pageNumber)
        {
            int nextPageNumber = pageNumber + 1;
            int maxPageBoundary = totalPageCount ?? int.MaxValue;

            if (nextPageNumber > cachedPageHighBoundary && cachedPageHighBoundary < maxPageBoundary)
            {
                int firstPageToLoad = nextPageNumber - cachePageSize + 1;
                if (firstPageToLoad <= cachedPageHighBoundary)
                    firstPageToLoad = cachedPageHighBoundary + 1;

                for (int page = firstPageToLoad; page <= nextPageNumber; page++)
                {
                    LoadPageNumber(page);
                }
                return;
            }

            int prevPageNumber = pageNumber > 0 ? pageNumber - 1 : 0;
            if (prevPageNumber < cachedPageLowBoundary)
            {
                int lastPageToLoad = prevPageNumber + cachePageSize - 1;
                if (lastPageToLoad >= cachedPageLowBoundary)
                    lastPageToLoad = cachedPageLowBoundary - 1;

                for (int page = lastPageToLoad; page >= prevPageNumber; page--)
                {
                    LoadPageNumber(page);
                }
            }
        }

        private DataPage LoadPageNumber(int pageNumber)
        {
            int boundary = MapToLowestIndex(RowsPerPage, pageNumber);
            var pageData = dataRetriever.SupplyPageOfData(boundary, RowsPerPage);
            var cachedPage = new DataPage(pageData, RowsPerPage, pageNumber);

            int maxIndex = cachedPage.HighestIndex + 1;
            if (maxIndex > maxCachedRowIndex)
                maxCachedRowIndex = maxIndex;

            if (!cachedPage.Full)
                SetTotalRowCount(maxCachedRowIndex);

            if (cachedPage.LoadedRows <= 0)
                return cachedPage;

            if (dataPagesCache.Count < cachePageSize)
                // Add some pages to cache after initially loading the file
                dataPagesCache.Add(cachedPage);
            else
            {
                // Replace the cached page furthest from the requested cell
                // with a new page containing the newly retrieved data.
                if (pageNumber < cachedPageLowBoundary)
                {
                    dataPagesCache.RemoveAt(cachePageSize - 1);
                    dataPagesCache.Insert(0, cachedPage);
                    cachedPageLowBoundary -= 1;
                }
                else if (pageNumber > cachedPageHighBoundary)
                {
                    dataPagesCache.RemoveAt(0);
                    dataPagesCache.Add(cachedPage);
                    cachedPageLowBoundary += 1;
                }
            }

            return cachedPage;
        }

        private bool IsPageNumberInPageCache(int pageNumber)
        {
            bool isCached = pageNumber >= cachedPageLowBoundary && pageNumber <= cachedPageHighBoundary;
            return isCached;
        }

        private object RetrieveElementFromPageCache(int cacheNumber, int rowIndex, int columnIndex)
        {
            DataPage cachedPage = dataPagesCache[cacheNumber];
            object colValue = RetrieveElementFromPage(cachedPage, rowIndex, columnIndex);
            return colValue;
        }

        DataRow lastRow = null;
        int lastRowIndex = -1;

        private object RetrieveElementFromPage(DataPage cachedPage, int rowIndex, int columnIndex)
        {
            int rowLocation = rowIndex % RowsPerPage;
            var rows = cachedPage.table.Rows;
            lastRow = rows[rowLocation];
            lastRowIndex = rowIndex;

            object colValue = lastRow[columnIndex];
            return colValue;
        }
        
        private void LoadFirstPages()
        {
            dataPagesCache = new List<DataPage>();

            for (int i = 0; i < cachePageSize; i++)
            {
                var cachedPage = LoadPageNumber(i);
                if (!cachedPage.Full)
                    break;
            }

            int? rows = dataRetriever.GetTotalRowCount();
            SetTotalRowCount(rows);
        }

        private void SetTotalRowCount(int? rows)
        {
            if (rows != null && totalRowCount == null)
            {
                totalRowCount = rows.Value;
                totalPageCount = MapToPageNumber(RowsPerPage, rows.Value);
            }
        }

        #endregion Retrieve and Cache

        #region Calculate Pages

        public int MapToCacheNumber(int pageNumber)
        {
            int cacheNumber = pageNumber - cachedPageLowBoundary;
            return cacheNumber;
        }

        public static int MapToPageNumber(int rowCount, int rowIndex)
        {
            // Return the number of a page containing the given index.
            return rowIndex / rowCount;
        }

        public static int MapToLowestIndex(int rowCount, int pageNumber)
        {
            // Return the lowest index of a page with the number.
            return pageNumber * rowCount;
        }

        public static int MapToHighestIndex(int rowCount, int pageNumber)
        {
            // Return the lowest index of a page with the number.
            int lowestIndexValue = MapToLowestIndex(rowCount, pageNumber);
            return lowestIndexValue + rowCount - 1;
        }

        public static int MapToLowerBoundary(int rowCount, int rowIndex)
        {
            // Return the lowest index of a page containing the given index.
            return (rowIndex / rowCount) * rowCount;
        }

        private static int MapToUpperBoundary(int rowCount, int rowIndex)
        {
            // Return the highest index of a page containing the given index.
            int lowestIndexValue = MapToLowerBoundary(rowCount, rowIndex);
            return lowestIndexValue + rowCount - 1;
        }

        #endregion Calculate Pages

        #region DataPage Structure

        // Represents one page of data.  
        protected class DataPage
        {
            public DataTable table;
            private int lowestIndexValue;
            private int loadedRows;
            private int rowsPerPage;
            private int pageNumber;

            public DataPage(int rowsPerPage, int pageNumber)
            {
                this.loadedRows = 0;
                this.rowsPerPage = rowsPerPage;
                this.pageNumber = pageNumber;
                this.lowestIndexValue = MapToLowestIndex(rowsPerPage, pageNumber);
            }

            public DataPage(DataTable table, int rowsPerPage, int pageNumber) : this(rowsPerPage, pageNumber)
            {
                this.table = table;
                loadedRows = table.Rows.Count;
            }

            public int LowestIndex { get { return lowestIndexValue; } }

            public int HighestIndex { get { return lowestIndexValue + loadedRows - 1; } }

            public int LoadedRows { get { return loadedRows; } }

            public int PageNumber { get { return pageNumber; } }

            public bool Full { get { return loadedRows >= rowsPerPage; } }

            public override string ToString()
            {
                string last = Full ? string.Empty : " (last)";

                return string.Format("#{0} Σ {1} Δ {2}-{3}{4}", 
                    pageNumber, loadedRows, lowestIndexValue, HighestIndex, last);
            }
        }

        #endregion DataPage Structure
    }
}
