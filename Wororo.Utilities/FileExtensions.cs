using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Wororo.Utilities;

/// <summary>
///     A collection of extension methods for working with files in C#
/// </summary>
public static class FileExtensions
{
    //private static readonly Regex CleanNameRegex = new Regex("[^a-zA-Z0-9_.]+", RegexOptions.Compiled);
    private static readonly Regex CleanNameAndSpacesRegex = new("[^a-zA-Z0-9_.\\- ]+", RegexOptions.Compiled);

    /// <summary>
    ///     Appends the given line to a file, creating the file if it doesn't exist yet.
    ///     If the file already exists, but is locked for writing, the method will wait and retry up to two more times before throwing an exception.
    /// </summary>
    /// <param name="filename">The name of the file to write to.</param>
    /// <param name="line">The line to append to the file.</param>
    public static void AppendSafe(string filename, string line)
    {
        AppendSafe(filename, new[] { line });
    }

    /// <summary>
    ///     Appends the given lines to a file, creating the file if it doesn't exist yet.
    ///     If the file already exists, but is locked for writing, the method will wait and retry up to two more times before throwing an exception.
    /// </summary>
    /// <param name="filename">The name of the file to write to.</param>
    /// <param name="lines">The lines to append to the file.</param>
    public static void AppendSafe(string filename, IEnumerable<string> lines)
    {
        if (!File.Exists(filename)) {
            filename.CreatePathIfNotExists();
            File.WriteAllLines(filename, lines);
        }

        try {
            File.AppendAllLines(filename, lines);
        }
        catch {
            Thread.Sleep(100);

            try {
                File.AppendAllLines(filename, lines);
            }
            catch {
                Thread.Sleep(100);
                File.AppendAllLines(filename, lines);
            }
        }
    }

    /// <summary>
    ///     Removes invalid characters and spaces from a filename, making it safe to use for file system operations.
    /// </summary>
    /// <param name="fileName">The name of the file to clean.</param>
    /// <returns>A cleaned version of the file name, with invalid characters and spaces removed.</returns>
    public static string CleanFileName(this string fileName)
    {
        return CleanNameAndSpacesRegex.Replace(fileName, string.Empty);
    }

    /// <summary>
    ///     Removes invalid characters from a filename, making it safe to use for file system operations.
    /// </summary>
    /// <param name="fileName">The name of the file to clean.</param>
    /// <returns>A cleaned version of the file name, with invalid characters removed.</returns>
    public static string CleanFileNameInvalidChars(this string fileName)

    {
        var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
        return Regex.Replace(fileName, invalidRegStr, string.Empty);
    }

    /// <summary>
    ///     Creates the directory structure for the given file path, if it doesn't exist yet.
    /// </summary>
    /// <param name="filepath">The file path to create the directories for.</param>
    public static void CreatePathIfNotExists(this string filepath)
    {
        var fileInfo = new FileInfo(filepath);

        if (!fileInfo.Directory.Exists) {
            fileInfo.Directory.Create();
        }
    }

    /// <summary>
    ///     Deletes a file or directory if it exists, and the additional condition is met.
    /// </summary>
    /// <param name="path">The path of the file or directory to delete.</param>
    /// <param name="additionalCondition">An additional condition that needs to be met in order to delete the file or directory.</param>
    public static void DeleteIfExists(this string path, bool additionalCondition = true)
    {
        if (Directory.Exists(path) && additionalCondition) Directory.Delete(path, true);

        if (File.Exists(path) && additionalCondition) File.Delete(path);
    }

    /// <summary>
    ///     Reads a stream as a sequence of lines.
    /// </summary>
    /// <param name="fileStream">The file stream to read from.</param>
    /// <returns>An enumerable sequence of strings, representing the lines of the file.</returns>
    public static IEnumerable<string> ReadLines(this Stream fileStream)
    {
        using var streamReader = new StreamReader(fileStream);

        while (!streamReader.EndOfStream) {
            yield return streamReader.ReadLine();
        }
    }

    /// <summary>
    ///     Reads a file as a sequence of lines.
    /// </summary>
    /// <param name="filename">The name of the file to read.</param>
    /// <returns>An enumerable sequence of strings, representing the lines of the file.</returns>
    public static IEnumerable<string> ReadLines(this string filename)
    {
        using var fileStream = File.OpenRead(filename);

        foreach (var line in ReadLines(fileStream)) {
            yield return line;
        }
    }

    //public static string GetFullPathWithoutExtension(this string path)
    //{
    //    return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
    //}

    /// <summary>
    ///     Restores a template file from the resources of an assembly, if it doesn't exist yet, or if the overwrite flag is set to true.
    /// </summary>
    /// <param name="assemblyFullName">The full name of the assembly containing the template file.</param>
    /// <param name="templateName">The name of the template file in the assembly's resources.</param>
    /// <param name="outputFilename">The name of the file to write the template contents to.</param>
    /// <param name="overwrite">A flag indicating whether to overwrite the output file if it already exists.</param>
    public static void RestoreTemplateFileIfDoesNotExists(string assemblyFullName, string templateName, string outputFilename, bool overwrite = false)
    {
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
