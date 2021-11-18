using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniExcelLibs;

namespace Wororo.Utilities
{
    public class ExcelBookModel
    {
        private readonly string _outputFilename;
        public ExcelBookModel(string outputFilename)
        {
            _outputFilename = outputFilename;
        }

        public IList<ExcelSheetModel> Sheets = new List<ExcelSheetModel>();

        public IDictionary<string, object> GetSheetsDictionary(bool addNewSheetWithWorkSheetColumn = false)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var sheet in Sheets)
            {
                dictionary.Add(sheet.SheetName, sheet.ToDictionary());
            }

            if (addNewSheetWithWorkSheetColumn)
            {
                var allRowsDictionary = new List<IDictionary<string, object>>();
                foreach (var sheet in Sheets)
                {
                    var rows = sheet.ToDictionary();
                    var newRows = rows.Select(x=>new Dictionary<string, object>(x)).ToArray();
                    foreach (var row in newRows)
                    {
                        row.Add("WorkSheet", sheet.SheetName);
                    }
                    allRowsDictionary.AddRange(newRows);
                }
                dictionary.Add("All", allRowsDictionary);
            }
            return dictionary;
        }

        public void Save(bool saveAll = false)
        {
            _outputFilename.CreatePathIfNotExists();
            _outputFilename.DeleteIfExists();

            MiniExcel.SaveAs(_outputFilename, GetSheetsDictionary(saveAll));
        }

        public void SaveAsTSV(bool naturalSortBefore = false)
        {
            foreach (var sheet in Sheets)
            {
                var header = sheet.Headers.ToTSV();
                var rows = new List<string>();
                foreach (var row in sheet.Rows)
                {
                    var values = row.Select(x => x.Value.ToString().ToSingleLineText()).ToTSV();
                    rows.Add(values);
                }

                if (naturalSortBefore)
                    rows = rows.NaturalSort().ToList();
                rows.Insert(0, header);
                File.WriteAllLines($"{Path.GetDirectoryName(_outputFilename)}{sheet.SheetName.CleanFileName().Replace(" ", "")}.tsv", rows);
            }
        }
    }
}