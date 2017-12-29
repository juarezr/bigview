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
            //var container = AvroContainer.CreateGenericReader(fileStream);
            //var block = container.Current;

            //var serializer = AvroSerializer.CreateGeneric();

            return ReadRecordsFromApache(fileStream, lowerPageBoundary, rowsPerPage);
        }

        protected  DataTable ReadRecordsFromApache(Stream fileStream, int lowerPageBoundary, int rowsPerPage)
        {

            if (dataFileReader == null)
            {
                dataFileReader = DataFileReader<GenericRecord>.OpenReader(fileStream);
                var metaKeys = dataFileReader.GetMetaKeys();
                foreach(string key in metaKeys)
                {
                    string prop = dataFileReader.GetMetaString(key);
                    SetProperty(key, prop);
                }
            }


            //var schema = (RecordSchema) dataFileReader.GetSchema();

            //var datumReader = new GenericReader<GenericRecord>(dataFileReader);
            //var datumReader = new GenericDatumReader<GenericRecord>(schema, schema);

            // var decoder = new BinaryDecoder(fileStream);


            DataTable table = null;

            //dataFileReader.
            // TODO : Avro -> Sync directly to the record number

            dataFileReader.Sync(0);

            int row = 0;
            //while (row < rowsPerPage && dataFileReader.HasNext())
            foreach (var record in dataFileReader.NextEntries)
            {
                row += 1;

                if (lowerPageBoundary > row)
                    continue;

                //GenericRecord record = dataFileReader.Next();
                //GenericRecord record = datumReader.Read(null, decoder);

                if (table == null)
                {
                    table = CreateDataTableFromSchema(record.Schema);
                }

                DataRow tableRow = table.NewRow();

                foreach (Field field in record.Schema.Fields)
                {
                    object value = GetValueFromField(record, field.Name);


                    if (value != null)
                        tableRow[field.Pos] = value;
                }

                table.Rows.Add(tableRow);

                if (row > rowsPerPage)
                    break;
            }

            return table;
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
