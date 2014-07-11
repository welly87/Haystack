using Haystack.ObjectStore;

namespace PhotoStore.Server
{
    public class InMemoryIndex
    {
        public long PhotoKey { get; set; }

        private readonly ImageInfo[] _imageInfo = new ImageInfo[4];

        public ImageInfo this[ImageSize index]
        {
            get { return _imageInfo[(int) index]; }
            private set
            {
                _imageInfo[(int) index] = value;
            }
        }

        private ImageInfo GetInfo(NeedleIndex[] needleIndex, ImageSize logicalSize)
        {
            return new ImageInfo { Size = needleIndex[(int)logicalSize].DataSize, Offset = needleIndex[(int)logicalSize].Offset };
        }

        public void AddAll(NeedleIndex[] needleIndex)
        {

            this[ImageSize.Large] = GetInfo(needleIndex, ImageSize.Large);
            this[ImageSize.Medium] = GetInfo(needleIndex, ImageSize.Medium);
            this[ImageSize.Small] = GetInfo(needleIndex, ImageSize.Small);
            this[ImageSize.Thumbnails] = GetInfo(needleIndex, ImageSize.Thumbnails);
        }
    }
}
