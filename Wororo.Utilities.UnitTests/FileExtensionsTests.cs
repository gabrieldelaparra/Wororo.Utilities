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
    }
}