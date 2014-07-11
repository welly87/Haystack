namespace PhotoStore.Server
{
    public interface IPhotoStoreService
    {
        void Start();
        void Store(long photoKey, int cookie, byte[][] allResizeResult);
        byte[] Load(int haystackId, long photoKey, ImageSize size, int cookie);
    }
}
