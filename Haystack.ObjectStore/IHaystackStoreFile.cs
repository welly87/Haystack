namespace Haystack.ObjectStore
{
    public interface IHaystackStoreFile
    {
        int Append(Needle needle);
        Needle Read(int offset, int size);
    }
}
