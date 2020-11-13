using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wororo.Utilities
{
    public static class FileExtensions
    {
        public static string CleanFileName(this string fileName) {
            return Regex.Replace(fileName, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
        }

        public static void CreatePathIfNotExists(this string filepath) {
            var fileInfo = new FileInfo(filepath);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
        }

        public static void DeleteIfExists(this string path, bool additionalCondition = true) {
            if (Directory.Exists(path) && additionalCondition) Directory.Delete(path, true);

            if (File.Exists(path) && additionalCondition) File.Delete(path);
        }

        public static IEnumerable<string> ReadLines(this Stream fileStream) {
            using var streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }

        public static IEnumerable<string> ReadLines(this string filename) {
            using var fileStream = File.OpenRead(filename);
            foreach (var line in ReadLines(fileStream))
                yield return line;
        }

        public static void RestoreTemplateFileIfDoesNotExists(string assemblyFullName, string templateName,
                string outputFilename, bool overwrite = false) {
            var f = new FileInfo(outputFilename);
            if (f.Directory != null && !f.Directory.Exists) f.Directory.Create();

            if (!File.Exists(outputFilename) || overwrite) {
                var assemblyList = AppDomain.CurrentDomain.GetAssemblies();

                var assembly = assemblyList.FirstOrDefault(x => x.FullName.Equals(assemblyFullName));

                if (assembly != null) {
                    assembly.GetManifestResourceNames();

                    using var templateStream = assembly.GetManifestResourceStream(templateName);
                    if (templateStream == null) return;
                    using var fileStream = File.Create(outputFilename);
                    templateStream.CopyTo(fileStream);
                }
            }
        }
    }
}