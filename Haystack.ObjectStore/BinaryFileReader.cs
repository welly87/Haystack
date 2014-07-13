using System.IO;

namespace Haystack.ObjectStore
{
    // TODO should be async IO
    internal class BinaryFileReader : IFileBinaryReader
    {
        private readonly BinaryReader _reader;

        public BinaryFileReader(string photosBin)
        {
            _reader = new BinaryReader(new FileStream("photos.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        public void Seek(int offset)
        {
            _reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        }

        public int ReadInt()
        {
            return _reader.ReadInt32();
        }

        public long ReadLong()
        {
            return _reader.ReadInt64();
        }

        public byte[] ReadData(int dataSize)
        {
            // TODO should avoid copy, should use MemoryStream instead.. ? reference only to the stream ? 
            var buffer = new byte[dataSize];

            _reader.Read(buffer, 0, dataSize);

            return buffer;
        }

        public void Close()
        {
            _reader.Close();
        }
    }
}