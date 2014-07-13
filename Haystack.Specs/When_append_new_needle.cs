using System.IO;
using Haystack.ObjectStore;
using NUnit.Framework;

namespace Haystack.Specs
{
    [TestFixture]
    public class When_append_new_needle
    {
        [Test]
        public void Should_able_to_retrieve_with_offset()
        {
            File.Delete("photos.bin");

            var storeFile = new HayStackStoreFile("photos.bin");
            
            var data = new byte[10000];
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

            var needle = storeFile.Read(offset, data.Length);

            Assert.AreEqual(tobesaved.Key, needle.Key);
            Assert.AreEqual(tobesaved.AlternateKey, needle.AlternateKey);
            Assert.AreEqual(tobesaved.Cookie, needle.Cookie);
            Assert.AreEqual(tobesaved.Data.Length, needle.Data.Length);
            Assert.AreEqual(tobesaved.DataCheckSum, needle.DataCheckSum);
            Assert.AreEqual(tobesaved.DataSize, needle.DataSize);
            Assert.AreEqual(tobesaved.Flags, needle.Flags);
            Assert.AreEqual(tobesaved.FooterMagicNumber, needle.FooterMagicNumber);
            Assert.AreEqual(tobesaved.HeaderMagicNumber, needle.HeaderMagicNumber);
            Assert.AreEqual(tobesaved.Padding, needle.Padding);

            storeFile.Close();
        }
    }
}
