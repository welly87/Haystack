using System.IO;
using Haystack.ObjectStore;
using NUnit.Framework;
using PhotoStore.Server;

namespace Haystack.Specs
{
    [TestFixture]
    public class When_write_two_photos_to_store
    {
        private const string StoreFile = "photos.bin";

        private const string TargetDir = "haystack_copied";

        private const string Image1 = "images1.jpg";

        private const string Image2 = "images2.jpg";

        [Test]
        public void Should_read_and_create_new_image_file()
        {
            File.Delete(StoreFile);

            if (Directory.Exists(TargetDir))
            {
                Directory.Delete(TargetDir, true);                
            }

            Directory.CreateDirectory(TargetDir);

            var storeFile = new HayStackStoreFile(StoreFile);

            var image1Info = SaveImage(storeFile, Path.Combine("images", Image1));

            var image2Info = SaveImage(storeFile, Path.Combine("images", Image2));

            storeFile.Close();

            var anotherStoreFile = new HayStackStoreFile(StoreFile);

            var needl1 = anotherStoreFile.Read(image1Info.Offset, image1Info.Size);

            var needl2 = anotherStoreFile.Read(image2Info.Offset, image2Info.Size);

            File.WriteAllBytes(Path.Combine(TargetDir, Image1), needl1.Data);
                                            
            File.WriteAllBytes(Path.Combine(TargetDir, Image2), needl2.Data);

            anotherStoreFile.Close();
        }

        private ImageInfo SaveImage(HayStackStoreFile storeFile, string image)
        {
            var data = File.ReadAllBytes(image);

            var tobesaved = new Needle
            {
                Key = 1L,
                AlternateKey = 1,
                Cookie = 1,
                Data = data,
                DataCheckSum = 1,
                DataSize = data.Length,
                Flags = 0,
                FooterMagicNumber = 0,
                HeaderMagicNumber = 0,
                Padding = 0
            };

            var offset = storeFile.Append(tobesaved);

            return new ImageInfo
            {
                Offset = offset, 
                Size = data.Length
            };
        }
    }
}
