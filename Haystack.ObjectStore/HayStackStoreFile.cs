namespace Haystack.ObjectStore
{
    // TODO should create async IO
    internal class HayStackStoreFile : IHaystackStoreFile
    {
        // TODO should more than one file for store file

        // TODO should separate writer and reader. For thread safety convenience
        private readonly IFileBinaryWriter _writer;

        // TODO reader should be independent of writer should not be shared
        private readonly IFileBinaryReader _reader;

        public HayStackStoreFile(string photosBin)
        {
            _writer = new BinaryFileWriter(photosBin);
            _reader = new BinaryFileReader(photosBin);
        }

        // TODO should check concurrency 
        public int Append(Needle needle)
        {
            // wrapper in the file 

            var offset = _writer.Position;

            WriteHeader(needle);

            WriteData(needle);
            
            WriteFooter(needle);

            _writer.Flush();

            return offset;
        }

        public Needle Read(int offset, int size)
        {
            _reader.Seek(offset);

            var needle = new Needle();

            ReadHeader(needle);

            ReadData(needle);

            ReadFooter(needle);

            return needle;
        }

        private void ReadFooter(Needle needle)
        {
            needle.FooterMagicNumber = _reader.ReadInt();

            needle.DataCheckSum = _reader.ReadInt();

            // TODO need to fix padding
            needle.Padding = _reader.ReadInt();
        }

        private void ReadData(Needle needle)
        {
            needle.Data = _reader.ReadData(needle.DataSize);
        }

        private void ReadHeader(Needle needle)
        {
            needle.HeaderMagicNumber = _reader.ReadInt();

            needle.Cookie = _reader.ReadInt();

            needle.Key = _reader.ReadLong();

            needle.AlternateKey = _reader.ReadInt();

            needle.Flags = _reader.ReadInt();

            needle.DataSize = _reader.ReadInt();
        }

        private void WriteFooter(Needle needle)
        {
            _writer.Write(needle.FooterMagicNumber); // 4 byte

            _writer.Write(needle.DataCheckSum); // 4 byte

            _writer.Write(needle.Padding); // 4 byte
        }

        private void WriteHeader(Needle needle)
        {
            _writer.Write(needle.HeaderMagicNumber); // 4 byte

            _writer.Write(needle.Cookie); // 4 byte

            _writer.Write(needle.Key); // 8 byte

            _writer.Write(needle.AlternateKey); // 4 byte

            _writer.Write(needle.Flags); // 4 byte

            _writer.Write(needle.DataSize); // 4 byte
        }

        private void WriteData(Needle needle)
        {
            _writer.Write(needle.Data);
        }

        public void Close()
        {
            _writer.Close();
            _reader.Close();
        }
    }

}