namespace Haystack.ObjectStore
{
    public interface IHaystackStoreService
    {
        NeedleIndex Write(long key, int alternateKey, int cookie, byte[] data);

        byte[] Read(long key, int alternateKey, int cookie, int offset, int dataSize);

        void Delete();
    }
}
