using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

// Based on: https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/virtual-mode-with-just-in-time-data-loading-in-the-datagrid

namespace VirtualDataTableLib
{
    public interface IDataPageRetriever : IDisposable
    {
        DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

        int? GetTotalRowCount();

        void OpenDataSource(string sourceAddress, int rowsPerPage);

        IDictionary<string, string> GetProperties();

    }

    public abstract class DataRetriever : IDataPageRetriever
    {
        private IDictionary<string, string> _properties = new Dictionary<string, string>();

        public abstract void Dispose();

        public abstract int? GetTotalRowCount();

        public abstract DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

        public abstract void OpenDataSource(string sourceAddress, int rowsPerPage);

        public IDictionary<string, string> GetProperties()
        {
            return _properties;
        }

        public void SetProperty(string key, string value)
        {
            _properties[key] = value;
        }
    }

    public abstract class FileDataRetriever : DataRetriever
    {
        protected Stream fileStream;

        public override void Dispose()
        {
            if (fileStream != null)
            {
                ((IDisposable)fileStream).Dispose();
                fileStream = null;
            }
        }

        public override void OpenDataSource(string sourceAddress, int rowsPerPage)
        {
            fileStream = new FileStream(sourceAddress, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage)
        {
            var table = ReadRecordsFrom(lowerPageBoundary, rowsPerPage);
            return table;
        }

        protected abstract DataTable ReadRecordsFrom(int lowerPageBoundary, int rowsPerPage);

    }


}
