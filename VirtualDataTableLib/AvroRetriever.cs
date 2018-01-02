using Avro;
using Avro.File;
using Avro.Generic;
using System.Data;

namespace VirtualDataTableLib
{
    public class AvroRetriever : FileDataRetriever
    {
        #region Properties

        private IFileReader<GenericRecord> dataFileReader = null;

        public override int? GetTotalRowCount()
        {
            return null; // tracked on cache
        }

        #endregion Properties

        #region Open/Dispose

        public override void OpenDataSource(string sourceAddress, int rowsPerPage)
        {
            base.OpenDataSource(sourceAddress, rowsPerPage);

            dataFileReader = DataFileReader<GenericRecord>.OpenReader(fileStream);
            var metaKeys = dataFileReader.GetMetaKeys();
            foreach (string key in metaKeys)
            {
                string prop = dataFileReader.GetMetaString(key);
                SetProperty(key, prop);
            }
        }

        public override void Dispose()
        {
            if (dataFileReader != null)
            {
                dataFileReader.Dispose();
                dataFileReader = null;
            }

            base.Dispose();
        }

        #endregion Open/Dispose

        #region Reading

        protected override DataTable ReadRecordsFrom(int lowerPageBoundary, int rowsPerPage)
        {
            return ReadRecordsFromApache(lowerPageBoundary, rowsPerPage);
        }

        private int lastLowerPageBoundary = int.MinValue;

        protected DataTable ReadRecordsFromApache(int lowerPageBoundary, int rowsPerPage)
        {
            // TODO: Handle snappy compression

            DataTable table = null;

            // TODO : Avro -> Sync directly to the record number

            if (lastLowerPageBoundary != lowerPageBoundary + rowsPerPage - 1)
                dataFileReader.Sync(0);

            lastLowerPageBoundary = lowerPageBoundary;

            int row = 0;
            int lastrow = lowerPageBoundary + rowsPerPage - 1;
            foreach (var record in dataFileReader.NextEntries)
            {
                if (row >= lowerPageBoundary)
                {
                    if (table == null)
                        table = CreateDataTableFromSchema(record.Schema);

                    ConvertToDataRow(table, record);
                }
                if (row >= lastrow)
                    break;
                row += 1;
            }

            return table;
        }

        private DataRow ConvertToDataRow(DataTable table, GenericRecord record)
        {
            DataRow tableRow = table.NewRow();

            foreach (Field field in record.Schema.Fields)
            {
                object value = GetValueFromField(record, field.Name);

                if (value != null)
                    tableRow[field.Pos] = value;
            }

            table.Rows.Add(tableRow);

            return tableRow;
        }

        private object GetValueFromField(GenericRecord record, string fieldname)
        {
            object value = record[fieldname];
            if (value != null)
                return value.ToString();

            return null;
        }

        private DataTable CreateDataTableFromSchema(RecordSchema schema)
        {
            var table = new DataTable();

            // TODO: Support nested types in Avro

            foreach (Avro.Field field in schema.Fields)
            {
                var col = new DataColumn(field.Name, typeof(string));
                table.Columns.Add(col);
            }


            return table;
        }

        #endregion Reading
    }
}
