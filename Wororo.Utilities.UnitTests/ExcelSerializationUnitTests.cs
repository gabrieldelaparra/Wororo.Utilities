using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
    public class ExcelSerializationUnitTests
    {
        [Test]
        public void TestStringEnumerableWithHeaderToDictionary()
        {
            var input = new[] {
                "A,B,C,D",
                "1,2,3,4",
                "5,6,7,8",
                "1,2,3,4"
            };

            var expected = new List<Dictionary<string, object>> {
                new Dictionary<string, object> { { "A", "1" }, { "B", "2" }, { "C", "3" }, { "D", "4" } },
                new Dictionary<string, object> { { "A", "5" }, { "B", "6" }, { "C", "7" }, { "D", "8" } },
                new Dictionary<string, object> { { "A", "1" }, { "B", "2" }, { "C", "3" }, { "D", "4" } }
            };

            var actual = ExcelSerialization.ToTableDictionary(input).ToList();

            Assert.AreEqual(expected.Count, actual.Count());
            Assert.AreEqual(expected.Count(x => x.ContainsKey("A")), actual.Count(x => x.ContainsKey("A")));
            Assert.AreEqual(expected.Count(x => x.ContainsKey("B")), actual.Count(x => x.ContainsKey("B")));
            Assert.AreEqual(expected.Count(x => x.ContainsKey("C")), actual.Count(x => x.ContainsKey("C")));
            Assert.AreEqual(expected.Count(x => x.ContainsKey("D")), actual.Count(x => x.ContainsKey("D")));


            for (var i = 0; i < actual.Count(); i++) {
                Assert.AreEqual(expected[i].Keys, actual[i].Keys);
                Assert.AreEqual(expected[i].Values, actual[i].Values);
            }
        }

        [Test]
        public void TestCreateSampleExcelFileFromDictionary()
        {
            var input = new[] {
                "A,B,C,D",
                "1,2,3,4",
                "5,6,7,8",
                "1,2,3,4"
            };

            ExcelSerialization.CSVToExcel(input, "test.xlsx");
        }

        [Test]
        public void TestCreateSampleExcelFileFromTwoSheetModel()
        {
            var input = new[] {
                "A,B,C,D",
                "1,2,3,4",
                "5,6,7,8",
                "1,2,3,4"
            };

            var excelBook = new ExportBook("testModel.xlsx");

            var sheet1 = new ExportSheet("Sheet1", new[] { "A", "B", "C", "D" });
            sheet1.AddRow(new[] { "1", "2", "3", "4" });
            sheet1.AddRow(new[] { "5", "6", "7", "8" });
            sheet1.AddRow(new[] { "1", "2", "3", "4" });
            excelBook.Sheets.Add(sheet1);

            var sheet2 = new ExportSheet("Sheet2", new[] { "A", "B", "C", "D" });
            sheet2.AddRow(new[] { "5", "6", "7", "8" });
            sheet2.AddRow(new[] { "1", "2", "3", "4" });
            sheet2.AddRow(new[] { "5", "6", "7", "8" });
            excelBook.Sheets.Add(sheet2);

            excelBook.ExportToExcel(true);
            excelBook.ExportToTSV();
        }
    }
}
