using System.IO;
using NUnit.Framework;

namespace Haystack.Specs
{
    [TestFixture]
    public class When_writing_byte
    {
        [Test]
        public void Should_write_into_correct_location()
        {
            File.Delete("binary.bin");

            var binaryWriter =
                new BinaryWriter(new FileStream("binary.bin", FileMode.Append, FileAccess.Write, FileShare.Read));

            Assert.AreEqual(0, binaryWriter.BaseStream.Position);

            binaryWriter.Write((byte)1);

            Assert.AreEqual(1, binaryWriter.BaseStream.Position);

            binaryWriter.BaseStream.Seek(1, SeekOrigin.Begin);

            Assert.AreEqual(1, binaryWriter.BaseStream.Position);
            
        }
    }
}
