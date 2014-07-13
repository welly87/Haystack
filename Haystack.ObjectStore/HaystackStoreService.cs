using System;

namespace Haystack.ObjectStore
{
    public class HaystackStoreService : IHaystackStoreService
    {
        private readonly IHaystackIndexFile _indexFile = new HaystackIndexFile();

        private readonly IHaystackStoreFile _storeFile = new HayStackStoreFile("photos.bin");

        private const int HeaderSize = 5*4 + 8;

        private const int FooterSize = 3*4;

        public NeedleIndex Write(long key, int alternateKey, int cookie, byte[] data)
        {
            // sync
            var offset = _storeFile.Append(new Needle
            {
                Key = key, 
                AlternateKey = alternateKey, 
                Cookie = cookie, 
                Data = data, 
                DataCheckSum = Checksum(data), 
                DataSize = data.Length, 
                Flags = 0, 
                FooterMagicNumber = 0, 
                HeaderMagicNumber = 0, 
                Padding = 0
            });

            var index = new NeedleIndex
            {
                Key = key,
                AlternateKey = alternateKey,
                DataSize = data.Length,
                Flags = 0,
                Offset = offset
            };

            // async / Fire and forget
            _indexFile.Append(index);

            return index;
        }

        private int Checksum(byte[] data)
        {

            // TODO change checksum
            return 0; 
        }

        public byte[] Read(long key, int alternateKey, int cookie, int offset, int dataSize)
        {
            var needleSize = HeaderSize + dataSize + FooterSize;

            var needle = _storeFile.Read(offset, needleSize);

            needle.VerifyValidRequest(key, alternateKey, cookie);

            return needle.Data;
        }


        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
