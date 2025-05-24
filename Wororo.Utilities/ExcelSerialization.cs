using MiniExcelLibs;

using MoreLinq;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wororo.Utilities;

public static class ExcelSerialization
{
    public static void CSVToExcel(IEnumerable<string> input, string outputFilename, bool hasHeader = true,
                                  char separator = ',', string sheetName = "Sheet1")
    {
        if (input == null || !input.Any()) {
            throw new ArgumentNullException("Empty input");
        }

        var first = input.First();
        var firstRow = first.Split(separator);
        var firstRowLength = firstRow.Length;

        if (!hasHeader) {
            var newHeader = Enumerable.Range(1, firstRowLength).Select(x => $"Column{x}");
            input.Insert(newHeader, 0);
        }

        var dictionary = ToTableDictionary(input, separator);

        ToExcel(dictionary, outputFilename, sheetName);
    }

    public static IEnumerable<IDictionary<string, object>> ToTableDictionary(IEnumerable<string> input,
                                                                             char separator = ',')
    {
        var (header, rows) = SplitHeaderAndRows(input, separator);
        var headerLength = header.Length;

        foreach (var row in rows) {
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < headerLength; i++) {
                dictionary.Add(header[i], row[i]);
            }

            yield return dictionary;
        }
    }

    private static (string[] header, IEnumerable<string[]> rows) SplitHeaderAndRows(IEnumerable<string> input,
                                                                                    char separator = ',')
    {
        var first = input.First();
        var header = first.Split(separator);
        var rows = input.Skip(1).Select(r => r.Split(separator));
        return (header, rows);
    }

    private static void ToExcel(IEnumerable<IDictionary<string, object>> dictionary, string outputFilename,
                                string sheetName = "Sheet1")
    {
        outputFilename.CreatePathIfNotExists();
        outputFilename.DeleteIfExists();

        MiniExcel.SaveAs(outputFilename, dictionary, sheetName: sheetName);
    }
}
