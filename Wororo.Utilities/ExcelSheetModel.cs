using System.Collections.Generic;
using System.Linq;

namespace Wororo.Utilities
{
    public class ExcelSheetModel
    {
        public ExcelSheetModel(string sheetName, IEnumerable<string> headers)
        {
            SheetName = sheetName;
            Headers = headers.ToArray();
            _headerLength = Headers.Length;
        }

        public string SheetName { get; set; }
        public string[] Headers { get; set; }
        private readonly int _headerLength;
        public IList<IDictionary<string, object>> Rows { get; set; }

        public void AddRow(IEnumerable<object> newRow)
        {
            if (Rows == null)
                Rows = new List<IDictionary<string, object>>();

            var row = newRow.ToArray();

            var dictionary = new Dictionary<string, object>();
            for (var i = 0; i < _headerLength; i++)
            {
                dictionary.Add(Headers[i], row[i]);
            }
            Rows.Add(dictionary);
        }

        public IDictionary<string, object>[] ToDictionary()
        {
            return Rows.ToArray();
        }


    }
}