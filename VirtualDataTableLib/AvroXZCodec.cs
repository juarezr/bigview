using SharpCompress.Compressors.Xz;
using System;
using System.IO;

namespace Avro.File
{
    public class AvroXZCodec : Codec
    {
        public override byte[] Compress(byte[] uncompressedData)
        {
            throw new NotImplementedException();
        }

        public override byte[] Decompress(byte[] compressedData)
        {
            var inStream = new MemoryStream(compressedData);
            var outStream = new MemoryStream();

            using (var decompress = new XZStream(inStream))
            {
                CopyTo(decompress, outStream);
            }
            return outStream.ToArray();
        }

        public override bool Equals(object other)
        {
            if (this == other)
                return true;
            return (this.GetType().Name == other.GetType().Name);
        }

        public override int GetHashCode()
        {
            return 1005;
        }

        public override string GetName()
        {
            return "xz";
        }

        #region Private Helpers

        private static void CopyTo(Stream from, Stream to)
        {
            byte[] buffer = new byte[4096];
            int read;
            while ((read = from.Read(buffer, 0, buffer.Length)) != 0)
            {
                to.Write(buffer, 0, read);
            }
        }

        #endregion Private Helpers
    }
}
