using System.Threading.Tasks;
using PhotoStore.Server;

namespace PhotoStore.API.Services
{
    internal class PhotoresizerService
    {
        private readonly PhotoResizer _resizer = new PhotoResizer();

        public Task<byte[][]> ResizeAll(byte[] photo)
        {
            var largeResizeTask = Task.Run(() => _resizer.Resize(photo, ImageSize.Large));

            var mediumResizeTask = Task.Run(() => _resizer.Resize(photo, ImageSize.Medium));

            var smallResizeTask = Task.Run(() => _resizer.Resize(photo, ImageSize.Small));

            var thumbnailsResizeTask = Task.Run(() => _resizer.Resize(photo, ImageSize.Thumbnails));

            return Task.WhenAll(largeResizeTask, mediumResizeTask, smallResizeTask, thumbnailsResizeTask);
        }
    }
}