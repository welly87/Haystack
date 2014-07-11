using System;
using System.Threading.Tasks;
using PhotoStore.Server;

namespace PhotoStore.API.Services
{
    public class PhotoStoreApi
    {
        private readonly IPhotoStoreService _photoStoreService = new PhotoStoreService();

        private readonly PhotoresizerService _resizerService = new PhotoresizerService();

        private Random _randomCookie = new Random();

        private long _uniqueId = 1;

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
            return _randomCookie.Next();
        }

        private long GenerateUniqueId()
        {
            // currently just autoincrement it !
            return _uniqueId++;
        }
    }
}
