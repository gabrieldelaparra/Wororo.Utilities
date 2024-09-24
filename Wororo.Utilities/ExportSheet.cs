using System.Collections.Generic;
using System.Linq;

namespace Wororo.Utilities;

/// <summary>
///     Represents an Excel sheet that can be exported as a dictionary.
/// </summary>
public class ExportSheet
{
    private readonly int _headerLength;

    /// <summary>
    ///     Initializes a new instance of the ExportSheet class with the specified sheet name and headers.
    /// </summary>
    /// <param name="sheetName">The name of the sheet.</param>
    /// <param name="headers">The headers to be included in the sheet.</param>
    public ExportSheet(string sheetName, IEnumerable<string> headers)
    {
        SheetName = sheetName;
        Headers = headers.ToArray();
        _headerLength = Headers.Length;
    }

    /// <summary>
    ///     Gets or sets the headers of the sheet.
    /// </summary>
    public string[] Headers { get; set; }

    /// <summary>
    ///     Gets or sets the rows of the sheet.
    /// </summary>
    public IList<IDictionary<string, object>> Rows { get; set; }

    /// <summary>
    ///     Gets or sets the name of the sheet.
    /// </summary>
    public string SheetName { get; set; }

    /// <summary>
    ///     Adds a new row to the sheet.
    /// </summary>
    /// <param name="newRow">The new row to be added.</param>
    public void AddRow(IEnumerable<object> newRow)
    {
        if (Rows == null) {
            Rows = new List<IDictionary<string, object>>();
        }

        var row = newRow.ToArray();

        var dictionary = new Dictionary<string, object>();

        for (var i = 0; i < _headerLength; i++) {
            dictionary.Add(Headers[i], row[i]);
        }

        Rows.Add(dictionary);
    }

    /// <summary>
    ///     Converts the sheet to an array of dictionaries.
    /// </summary>
    /// <returns>An array of dictionaries representing the sheet.</returns>
    public IDictionary<string, object>[] ToDictionary()
    {
        return Rows.ToArray();
    }
}
