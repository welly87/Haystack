namespace Haystack.ObjectStore
{
    internal class HayStackStoreFile : IHaystackStoreFile
    {
        readonly IBinaryWriter _writer = new SimpleBinaryWriter();

        public HayStackStoreFile()
        {
            // load file if exist.. 

            // if file not exist then create a new file

            // write superblock
        }

        public int Append(Needle needle)
        {
            // wrapper in the file 

            WriteHeader(needle);

            _writer.Write(needle.Data);

            WriteFooter(needle);

            return 0;
        }

        public Needle Read(int offset, int size)
        {
            throw new System.NotImplementedException();
        }

        private void WriteFooter(Needle needle)
        {
            _writer.Write(needle.FooterMagicNumber);

            _writer.Write(needle.DataCheckSum);

            _writer.Write(needle.Padding);
        }

        private void WriteHeader(Needle needle)
        {
            _writer.Write(needle.HeaderMagicNumber);

            _writer.Write(needle.Cookie);

            _writer.Write(needle.AlternateKey);

            _writer.Write(needle.Flags);

            _writer.Write(needle.DataSize);
        }
    }
}