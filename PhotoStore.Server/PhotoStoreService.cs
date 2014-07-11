using Haystack.ObjectStore;

namespace PhotoStore.Server
{
    public class PhotoStoreService : IPhotoStoreService
    {
        private readonly IHaystackIndexFile _indexFile = new HaystackIndexFile();

        private readonly InMemoryIndexStorage _inMemoryIndex = new InMemoryIndexStorage();

        private readonly IHaystackStoreService _haystackService = new HaystackStoreService();

        public void Start()
        {
            // populate index at the startup into memory
            _inMemoryIndex.SaveAll(_indexFile.AllIndex);
        }

        public void Store(int uniqueId, int cookie, byte[][] allResizeResult)
        {
            // for now always assume 4 size (small, thumb, medium and large)

            var indexes = new NeedleIndex[4];

            for (var i = 0; i < allResizeResult.Length; i++)
            {
                // should be single thread access per file... ACTOR
                var index = _haystackService.Write(uniqueId, i, cookie, allResizeResult[0]);

                indexes[0] = index;
            }

            _inMemoryIndex.Save(indexes);
        }

        public byte[] Load(int haystackId, long photoKey, ImageSize size, int cookie)
        {
            var idx = _inMemoryIndex.Get(photoKey);

            var offset = idx[size].Offset;

            var dataSize = idx[size].Size;

            return _haystackService.Read(idx.PhotoKey, (int) size, cookie, offset, dataSize);
        }
    }
}
