
// Based on: https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/virtual-mode-with-just-in-time-data-loading-in-the-datagrid


using System.Collections.Generic;
using System.Data;

namespace VirtualDataTableLib
{
    public class DataTableCache
    {
        #region Init

        private static int RowsPerPage;
        private DataPage[] cachePages;
        private IDataPageRetriever dataRetriever;

        public DataTableCache(IDataPageRetriever dataSupplier, int rowsPerPage)
        {
            dataRetriever = dataSupplier;
            DataTableCache.RowsPerPage = rowsPerPage;
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
            return cache;
        }

        #endregion Init

        #region Properties

        public DataColumnCollection GetFields()
        {
            LoadFirstTwoPages();

            DataPage cachedPage = cachePages[0];
            var cols = cachedPage.table.Columns;
            return cols;
        }

        public long? GetTotalRowCount()
        {
            return dataRetriever.GetTotalRowCount();
        }

        public IDictionary<string, string> GetProperties()
        {
            LoadFirstTwoPages();
            
            return dataRetriever.GetProperties();
        }

        #endregion Properties

        #region Retrieve and Cache

        public string RetrieveElement(int rowIndex, int columnIndex)
        {
            LoadFirstTwoPages();

            string element;
            bool isCached = IfPageCached_ThenSetElement(rowIndex, columnIndex, out element);
            if (isCached)
            {
                return element;
            }
            else
            {
                string loading = RetrieveData_CacheIt_ThenReturnElement(rowIndex, columnIndex);
                return loading;
            }
        }

        private void LoadFirstTwoPages()
        {
            if (cachePages != null)
                return;

            int boundary1 = DataPage.MapToLowerBoundary(0);

            var pageData1 = dataRetriever.SupplyPageOfData(boundary1, RowsPerPage);

            var page1 = new DataPage(pageData1, 0);

            int boundary2 = DataPage.MapToLowerBoundary(RowsPerPage);

            var pageData2 = dataRetriever.SupplyPageOfData(boundary2, RowsPerPage);

            var page2 = new DataPage(pageData2, RowsPerPage);

            cachePages = new DataPage[] { page1, page2 };
        }

        // Sets the value of the element parameter if the value is in the cache.
        private bool IfPageCached_ThenSetElement(int rowIndex, int columnIndex, out string element)
        {
            element = string.Empty;
            bool isCachedIn0 = IsRowCachedInPage(0, rowIndex);
            if (isCachedIn0)
            {
                element = RetrieveElementFromCache(0, rowIndex, columnIndex);
                return true;
            }
            else
            {
                bool isCachedIn1 = IsRowCachedInPage(1, rowIndex);
                if (isCachedIn1)
                {
                    element = RetrieveElementFromCache(1, rowIndex, columnIndex);
                    return true;
                }
            }
            return false;
        }

        // Returns a value indicating whether the given row index is contained
        // in the given DataPage. 
        private bool IsRowCachedInPage(int pageNumber, int rowIndex)
        {
            DataPage cachedPage = cachePages[pageNumber];

            bool isLower = rowIndex <= cachedPage.HighestIndex;
            bool isHigher = rowIndex >= cachedPage.LowestIndex;

            return isLower && isHigher;
        }

        private string RetrieveElementFromCache(int cacheNumber, int rowIndex, int columnIndex)
        {
            DataPage cachedPage = cachePages[cacheNumber];
            int rowLocation = rowIndex % RowsPerPage;
            var row = cachedPage.table.Rows[rowLocation];
            var rowValue = row[columnIndex];

            string element = rowValue == null ? string.Empty : rowValue.ToString();
            return element;
        }

        private string RetrieveData_CacheIt_ThenReturnElement(
            int rowIndex, int columnIndex)
        {
            // Retrieve a page worth of data containing the requested value.
            int boundaryRow = DataPage.MapToLowerBoundary(rowIndex);

            DataTable table = dataRetriever.SupplyPageOfData(boundaryRow, RowsPerPage);

            // Replace the cached page furthest from the requested cell
            // with a new page containing the newly retrieved data.
            int unusedPageIndex = GetIndexToUnusedPage(rowIndex);
            cachePages[unusedPageIndex] = new DataPage(table, rowIndex);

            string element = RetrieveElement(rowIndex, columnIndex);
            return element;
        }

        // Returns the index of the cached page most distant from the given index
        // and therefore least likely to be reused.
        private int GetIndexToUnusedPage(int rowIndex)
        {
            var cachedPage0 = cachePages[0];
            var cachedPage1 = cachePages[1];

            if (rowIndex > cachedPage0.HighestIndex &&
                rowIndex > cachedPage1.HighestIndex)
            {
                int offsetFromPage0 = rowIndex - cachedPage0.HighestIndex;
                int offsetFromPage1 = rowIndex - cachedPage1.HighestIndex;
                if (offsetFromPage0 < offsetFromPage1)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                int offsetFromPage0 = cachedPage0.LowestIndex - rowIndex;
                int offsetFromPage1 = cachedPage1.LowestIndex - rowIndex;
                if (offsetFromPage0 < offsetFromPage1)
                {
                    return 1;
                }
                return 0;
            }

        }

        #endregion Retrieve and Cache

        #region DataPage Structure

        // Represents one page of data.  
        protected struct DataPage
        {
            public DataTable table;
            private int lowestIndexValue;
            private int highestIndexValue;

            public DataPage(DataTable table, int rowIndex)
            {
                this.table = table;
                lowestIndexValue = MapToLowerBoundary(rowIndex);
                highestIndexValue = MapToUpperBoundary(rowIndex);
                System.Diagnostics.Debug.Assert(lowestIndexValue >= 0);
                System.Diagnostics.Debug.Assert(highestIndexValue >= 0);
            }

            public int LowestIndex
            {
                get
                {
                    return lowestIndexValue;
                }
            }

            public int HighestIndex
            {
                get
                {
                    return highestIndexValue;
                }
            }

            public static int MapToLowerBoundary(int rowIndex)
            {
                // Return the lowest index of a page containing the given index.
                return (rowIndex / RowsPerPage) * RowsPerPage;
            }

            private static int MapToUpperBoundary(int rowIndex)
            {
                // Return the highest index of a page containing the given index.
                return MapToLowerBoundary(rowIndex) + RowsPerPage - 1;
            }
        }

        #endregion DataPage Structure
    }
}
