using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
    public class FileExtensionsTests
    {
        [Test]
        public void TestCleanFileName() {
            Assert.AreEqual("Single1.CFG", "Single1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "Sin?gle1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", ":Single1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "/Single1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "Sin\\gle1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "Sin*gle1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "Sin<gle1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "|Sin<*gle1.CFG".CleanFileName());
            Assert.AreEqual("Single1.CFG", "|Single1.CFG".CleanFileName());
        }

        [Test]
        [Platform(Exclude = "Linux,Unix,MacOsX")]
        public void TestCleanFileNameInvalidChars()
        {
            Assert.AreEqual("Single1.CFG", "Single1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "Sin?gle1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", ":Single1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "/Single1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "Sin\\gle1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "Sin*gle1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "Sin<gle1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "|Sin<*gle1.CFG".CleanFileNameInvalidChars());
            Assert.AreEqual("Single1.CFG", "|Single1.CFG".CleanFileNameInvalidChars());
        }

        [Test]
        public void TestGetOrCreateDirectory() {
            const string dirPath = "TestGetOrCreate/";
            var filePath = $"{dirPath}file.ext";

            if (Directory.Exists(dirPath))
                Directory.Delete(dirPath);

            Assert.False(Directory.Exists(dirPath));

            filePath.CreatePathIfNotExists();

            Assert.True(Directory.Exists(dirPath));

            if (Directory.Exists(dirPath))
                Directory.Delete(dirPath);
        }

        [Test]
        public void TestReadLines() {
            const string filename = "Resources/ReadFileTenLines.nt";

            Assert.True(File.Exists(filename));

            var readLines = filename.ReadLines().ToArray();
            Assert.NotNull(readLines);
            Assert.IsNotEmpty(readLines);
            Assert.AreEqual(10, readLines.Count());
        }

        [Test]
        public void TestDeleteIfExistsDirectory()
        {
            var path = "tempFolder";

            path.DeleteIfExists();
            Assert.False(Directory.Exists(path));

            Directory.CreateDirectory(path);
            Assert.True(Directory.Exists(path));

            path.DeleteIfExists();
            Assert.False(Directory.Exists(path));

            path.DeleteIfExists();
            Assert.False(Directory.Exists(path));
        }

        [Test]
        public void TestDeleteIfExistsFile()
        {
            var path = "tempFile";

            path.DeleteIfExists();
            Assert.False(File.Exists(path));

            using (var file = File.Create(path))
            {
            }

            Assert.True(File.Exists(path));

            path.DeleteIfExists();
            Assert.False(File.Exists(path));

            path.DeleteIfExists();
            Assert.False(File.Exists(path));
        }
    }
}