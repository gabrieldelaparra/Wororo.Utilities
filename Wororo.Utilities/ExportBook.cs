using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniExcelLibs;

namespace Wororo.Utilities;

/// <summary>
///     Represents a book of sheets to be exported to either Excel or TSV format.
/// </summary>
public class ExportBook
{
    private readonly string _outputFilename;

    /// <summary>
    ///     List of sheets to be included in the book.
    /// </summary>
    public IList<ExportSheet> Sheets = new List<ExportSheet>();

    public ExportBook(string outputFilename)
    {
        _outputFilename = outputFilename;
    }

    /// <summary>
    ///     Exports the book to an Excel file.
    /// </summary>
    /// <param name="createAllSummarySheet">Whether or not to create a summary sheet containing all rows from each sheet.</param>
    public void ExportToExcel(bool createAllSummarySheet = false)
    {
        _outputFilename.CreatePathIfNotExists();
        _outputFilename.DeleteIfExists();

        MiniExcel.SaveAs(_outputFilename, GetSheetsDictionary(createAllSummarySheet));
    }

    /// <summary>
    ///     Exports each sheet in the book to a TSV file.
    /// </summary>
    /// <param name="naturalSortBefore">Whether or not to sort rows naturally before exporting.</param>
    public void ExportToTSV(bool naturalSortBefore = false)
    {
        foreach (var sheet in Sheets) {
            var header = sheet.Headers.ToTSV();
            var rows = new List<string>();

            foreach (var row in sheet.Rows) {
                var values = row.Select(x => x.Value?.ToString()?.ToSingleLineText() ?? string.Empty).ToTSV();
                rows.Add(values);
            }

            if (naturalSortBefore) {
                rows = rows.NaturalSort().ToList();
            }

            rows.Insert(0, header);

            File.WriteAllLines(
                $"{Path.GetDirectoryName(_outputFilename)}\\{sheet.SheetName.CleanFileName().Replace(" ", "")}.tsv",
                rows);
        }
    }

    /// <summary>
    ///     Gets a dictionary of the sheets included in the book.
    /// </summary>
    /// <param name="createAllSummarySheet">Whether or not to create a summary sheet containing all rows from each sheet.</param>
    /// <returns>A dictionary of sheets included in the book.</returns>
    public IDictionary<string, object> GetSheetsDictionary(bool createAllSummarySheet = false)
    {
        var dictionary = new Dictionary<string, object>();

        foreach (var sheet in Sheets) {
            dictionary.Add(sheet.SheetName, sheet.ToDictionary());
        }

        if (createAllSummarySheet) {
            var allRowsDictionary = new List<IDictionary<string, object>>();

            foreach (var sheet in Sheets) {
                var rows = sheet.ToDictionary();
                var newRows = rows.Select(x => new Dictionary<string, object>(x)).ToArray();

                foreach (var row in newRows) {
                    row.Add("WorkSheet", sheet.SheetName);
                }

                allRowsDictionary.AddRange(newRows);
            }

            dictionary.Add("All", allRowsDictionary);
        }

        return dictionary;
    }
}
