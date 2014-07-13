using System;
using System.IO;
using NUnit.Framework;

namespace Haystack.Specs
{
    [TestFixture]
    public class When_writing_binary_to_file
    {
        [Test]
        public void Should_write_binary_int()
        {
            File.Delete("binary.bin");

            var binaryWriter =
                new BinaryWriter(new FileStream("binary.bin", FileMode.Append, FileAccess.Write, FileShare.Read));

            binaryWriter.Write(1);

            binaryWriter.Close();

            var fileInfo = new FileInfo("binary.bin");
            Assert.AreEqual(4, fileInfo.Length);
        }

        [Test]
        public void Should_read_int_as_a_byte()
        {
            File.Delete("binary.bin");

            var binaryWriter =
                new BinaryWriter(new FileStream("binary.bin", FileMode.Append, FileAccess.Write, FileShare.Read));

            binaryWriter.Write(1);

            binaryWriter.Close();

            var binaryReader =
                new BinaryReader(new FileStream("binary.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            
            var buffer = new byte[4];

            binaryReader.Read(buffer, 0, 4);

            Assert.AreEqual(1, BitConverter.ToInt32(buffer, 0));

            binaryReader.Close();

        }
    }
}
