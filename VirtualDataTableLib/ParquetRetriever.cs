using Parquet;
using Parquet.Data;
using System;
using System.Data;
using System.IO;

namespace VirtualDataTableLib
{
    public class ParquetRetriever : FileDataRetriever
    {
        private long? _totalRowCount;

        public override long? GetTotalRowCount()
        {
            return _totalRowCount;
        }

        protected override DataTable ReadRecordsFrom(Stream fileStream, int lowerPageBoundary, int rowsPerPage)
        {
            var options = new ParquetOptions
            {
                TreatByteArrayAsString = true
            };

            var readOptions = new ReaderOptions
            {
                Offset = lowerPageBoundary,
                Count = rowsPerPage
            };

            var records = ParquetReader.Read(fileStream, options, readOptions);

            _totalRowCount = records.TotalRowCount;

            var dataTable = convertToDataTable(records);

            return dataTable;
        }

        private DataTable convertToDataTable(Parquet.Data.DataSet records)
        {
            DataTable table = CreateDataTableFromParquet(records);

            for (int r = 0; r < records.RowCount; r++)
            {
                Row row = records[r];

                DataRow tableRow = table.NewRow();

                for (int f = 0; f < records.FieldCount; f++)
                {
                    object value = GetValueFromField(row, f);

                    if (value != null)
                        tableRow[f] = value;
                }

                table.Rows.Add(tableRow);
            }


            return table;
        }

        private static DataTable CreateDataTableFromParquet(Parquet.Data.DataSet records)
        {
            var table = new DataTable();

            // TODO: Support nested types in Parquet

            for (int f = 0; f < records.FieldCount; f++)
            {
                Field field = records.Schema[f];

                Type schemaType = GetTypeFromField(field);

                var col = new DataColumn(field.Name, schemaType);
                table.Columns.Add(col);
            }

            return table;
        }

        private object GetValueFromField(Row row, int f)
        {
            if (!row.IsNullAt(f))
            {
                object value = row.RawValues[f];
                if (value != null)
                    return value.ToString();
            }
            return null;
        }

        private static Type GetTypeFromField(Field field)
        {
            Type stringType = typeof(string);

            //if (field.SchemaType == Parquet.Data.SchemaType.Data)
            //{
            //    var dataField = (Parquet.Data.DataField)field;
            //    var schemaType = dataField.DataType;
            //}

            return stringType;
        }
    }
}
