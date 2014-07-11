using System;
using System.Threading.Tasks;
using PhotoStore.Server;

namespace PhotoStore.API.Services
{
    public class PhotoStoreApi
    {
        private readonly IPhotoStoreService _photoStoreService = new PhotoStoreService();

        private readonly PhotoresizerService _resizerService = new PhotoresizerService();

        public async Task UploadPhoto(int userId, byte[] photo)
        {
            var uniqueId = GenerateUniqueId();

            var cookie = GenerateRandomCookie();

            var allResizeResult = await _resizerService.ResizeAll(photo);

            _photoStoreService.Store(uniqueId, cookie, allResizeResult);
        }

        public byte[] ReadPhoto()
        {
            //return _photoStoreService.Load()
            return null;
        }

        private int GenerateRandomCookie()
        {
            throw new NotImplementedException();
        }

        // Should use Guid though...
        private int GenerateUniqueId()
        {
            throw new NotImplementedException();
        }
    }
}
