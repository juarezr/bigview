using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

// Based on: https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/virtual-mode-with-just-in-time-data-loading-in-the-datagrid

namespace VirtualDataTableLib
{
    public interface IDataPageRetriever
    {
        DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

        long? GetTotalRowCount();

        void OpenDataSource(string sourceAddress);

        IDictionary<string, string> GetProperties();
    }

    public abstract class DataRetriever : IDataPageRetriever
    {
        private IDictionary<string, string> _properties = new Dictionary<string, string>();

        public abstract long? GetTotalRowCount();

        public abstract DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

        public abstract void OpenDataSource(string sourceAddress);

        public IDictionary<string, string> GetProperties()
        {
            return _properties;
        }

        public void SetProperty(string key, string value)
        {
            _properties[key] = value;
        }
    }

    public abstract class FileDataRetriever : DataRetriever, IDisposable
    {
        private Stream fileStream;

        public virtual void Dispose()
        {
            if (fileStream != null)
                ((IDisposable)fileStream).Dispose();
        }

        public override void OpenDataSource(string sourceAddress)
        {
            fileStream = new FileStream(sourceAddress, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage)
        {
            var table = ReadRecordsFrom(fileStream, lowerPageBoundary, rowsPerPage);
            return table;
        }

        protected abstract DataTable ReadRecordsFrom(Stream fileStream, int lowerPageBoundary, int rowsPerPage);
    }
}
