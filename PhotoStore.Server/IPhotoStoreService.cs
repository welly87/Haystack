namespace PhotoStore.Server
{
    public interface IPhotoStoreService
    {
        void Start();
        void Store(int uniqueId, int cookie, byte[][] allResizeResult);
        byte[] Load(int haystackId, long photoKey, ImageSize size, int cookie);
    }
}
