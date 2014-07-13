using System.IO;
using NUnit.Framework;

namespace Haystack.Specs
{
    [TestFixture]
    public class When_write_with_file_stream
    {
        [Test]
        public void Should_append_bytes_to_stream()
        {
            File.Delete("myfile.dat");

            var fileStream = new FileStream("myfile.dat", FileMode.Append, FileAccess.Write, FileShare.Read);

            fileStream.WriteAsync(new byte[10000], 0, 10000).Wait();

            Assert.AreEqual(10000, fileStream.Position);

            fileStream.Close();

            fileStream = new FileStream("myfile.dat", FileMode.Append, FileAccess.Write, FileShare.Read); 

            Assert.AreEqual(10000, fileStream.Position);

            fileStream.WriteAsync(new byte[10000], 0, 10000).Wait();

            Assert.AreEqual(20000, fileStream.Position);

            fileStream.Close();
        }

        [Test]
        public void Should_write_and_read_the_same_file()
        {
            File.Delete("myfile.dat");

            var writeStream = new FileStream("myfile.dat", FileMode.Append, FileAccess.Write, FileShare.Read);

            writeStream.WriteAsync(new byte[10000], 0, 10000).Wait();

            var readStream = new FileStream("myfile.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            var count = readStream.Read(new byte[readStream.Length], 0, (int)readStream.Length);

            Assert.AreEqual(writeStream.Position, count);

            writeStream.WriteAsync(new byte[10000], 0, 10000).Wait();

            count = readStream.Read(new byte[readStream.Length], 0, (int)readStream.Length);

            Assert.AreEqual(10000, count);

            writeStream.Close();

            readStream.Close();
        }
    }
}
