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
    }

    public abstract class DataRetriever : IDataPageRetriever
    {
        public abstract long? GetTotalRowCount();

        public abstract DataTable SupplyPageOfData(int lowerPageBoundary, int rowsPerPage);

        public abstract void OpenDataSource(string sourceAddress);

    }

    public abstract class FileDataRetriever : DataRetriever
    {
        private Stream fileStream;

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
