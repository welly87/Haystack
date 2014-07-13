using System.IO;

namespace Haystack.ObjectStore
{
    // TODO should we use MemoryMappedFile ? 

    // TODO check how event store, kafka working

    // TODO should create async IO
    internal class BinaryFileWriter : IBinaryWriter
    {
        private readonly BinaryWriter _writer;
        public BinaryFileWriter(string photosBin)
        {
            // load file if exist.. 

            // if file not exist then create a new file

            _writer = new BinaryWriter(new FileStream("photos.bin", FileMode.Append, FileAccess.Write, FileShare.Read));

            EnsureSuperBlockExists();
        }

        private void EnsureSuperBlockExists()
        {
            // new file
            if (_writer.BaseStream.Position != 0) return;

            WriteSuperBlock();
        }

        private void WriteSuperBlock()
        {
            _writer.Write(GenerateSuperBlock());
        }

        private byte[] GenerateSuperBlock()
        {
            return new byte[1024 * 8];
        }

        public void Write(int i)
        {
            _writer.Write(i);
        }

        public void Write(long i)
        {
            _writer.Write(i);
        }

        public void Write(byte[] data)
        {
            _writer.Write(data);
        }

        public int Position
        {
            get
            {
                return (int) _writer.BaseStream.Position;
            }
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public void Close()
        {
            _writer.Close();
        }
    }
}