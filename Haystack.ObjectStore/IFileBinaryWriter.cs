namespace Haystack.ObjectStore
{
    internal interface IFileBinaryWriter
    {
        void Write(int i);
        void Write(long i);
        void Write(byte[] data);
        int Position { get;  }
        void Flush();
        void Close();
    }
}