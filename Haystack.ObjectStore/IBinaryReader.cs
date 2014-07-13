namespace Haystack.ObjectStore
{

    // TODO this one should be stateless ? or single actor ?? 
    internal interface IBinaryReader
    {
        void Seek(int offset);
        int ReadInt();
        long ReadLong();
        byte[] ReadData(int dataSize);
        void Close();
    }
}