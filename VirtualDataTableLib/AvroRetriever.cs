using Avro;
using Avro.File;
using Avro.Generic;
using System.Data;
using System.IO;

namespace VirtualDataTableLib
{
    public class AvroRetriever : FileDataRetriever
    {
        private IFileReader<GenericRecord> dataFileReader = null;

        public override int? GetTotalRowCount()
        {
            return null;
        }

        protected override DataTable ReadRecordsFrom(Stream fileStream, int lowerPageBoundary, int rowsPerPage)
        {
            return ReadRecordsFromApache(fileStream, lowerPageBoundary, rowsPerPage);
        }

        protected DataTable ReadRecordsFromApache(Stream fileStream, int lowerPageBoundary, int rowsPerPage)
        {
            // TODO: Handle snappy compression

            if (dataFileReader == null)
            {
                dataFileReader = DataFileReader<GenericRecord>.OpenReader(fileStream);
                var metaKeys = dataFileReader.GetMetaKeys();
                foreach (string key in metaKeys)
                {
                    string prop = dataFileReader.GetMetaString(key);
                    SetProperty(key, prop);
                }
            }


            DataTable table = null;

            // TODO : Avro -> Sync directly to the record number

            dataFileReader.Sync(0);

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

        public override void Dispose()
        {
            if (dataFileReader != null)
                dataFileReader.Dispose();

            base.Dispose();
        }
    }
}
