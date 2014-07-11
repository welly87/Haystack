namespace Haystack.ObjectStore
{
    internal interface IBinaryWriter
    {
        void Write(int i);
        void Write(long i);
        void Write(byte[] data);
    }
}