using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Wororo.Utilities
{
    public static class FileExtensions
    {
        //private static readonly Regex CleanNameRegex = new Regex("[^a-zA-Z0-9_.]+", RegexOptions.Compiled);
        private static readonly Regex CleanNameAndSpacesRegex = new Regex("[^a-zA-Z0-9_.\\- ]+", RegexOptions.Compiled);
        public static void AppendSafe(string filename, string line)
        {
            AppendSafe(filename, new[] {line});
        }

        public static void AppendSafe(string filename, IEnumerable<string> lines)
        {
            if (!File.Exists(filename))
            {
                filename.CreatePathIfNotExists();
                File.WriteAllLines(filename, lines);
            }

            try
            {
                File.AppendAllLines(filename, lines);
            }
            catch
            {
                Thread.Sleep(100);
                try
                {
                    File.AppendAllLines(filename, lines);
                }
                catch
                {
                    Thread.Sleep(100);
                    File.AppendAllLines(filename, lines);
                }
            }
        }

        public static string CleanFileNameInvalidChars(this string fileName)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            return Regex.Replace(fileName, invalidRegStr, string.Empty);
        }

        public static string CleanFileName(this string fileName)
        {
            return CleanNameAndSpacesRegex.Replace(fileName, string.Empty);
        }

        public static void CreatePathIfNotExists(this string filepath)
        {
            var fileInfo = new FileInfo(filepath);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
        }

        public static void DeleteIfExists(this string path, bool additionalCondition = true)
        {
            if (Directory.Exists(path) && additionalCondition) Directory.Delete(path, true);

            if (File.Exists(path) && additionalCondition) File.Delete(path);
        }

        public static IEnumerable<string> ReadLines(this Stream fileStream)
        {
            using var streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }

        public static IEnumerable<string> ReadLines(this string filename)
        {
            using var fileStream = File.OpenRead(filename);
            foreach (var line in ReadLines(fileStream))
                yield return line;
        }

        public static void RestoreTemplateFileIfDoesNotExists(string assemblyFullName, string templateName,
            string outputFilename, bool overwrite = false)
        {
            var f = new FileInfo(outputFilename);
            if (f.Directory != null && !f.Directory.Exists) f.Directory.Create();

            if (!File.Exists(outputFilename) || overwrite)
            {
                var assemblyList = AppDomain.CurrentDomain.GetAssemblies();

                var assembly = assemblyList.FirstOrDefault(x => x.FullName.Equals(assemblyFullName));

                if (assembly != null)
                {
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