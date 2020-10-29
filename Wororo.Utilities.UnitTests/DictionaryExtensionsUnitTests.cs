using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
    public class DictionaryExtensionsUnitTests
    {
        [Test]
        public void TestAddSafeDifferentTypes() {
            var dictionary = new Dictionary<int, List<string>>();

            dictionary.AddSafe(1, "1");
            Assert.AreEqual(1, dictionary[1].Count);
            Assert.AreEqual("1", dictionary[1].ElementAt(0));

            dictionary.AddSafe(1, "2");
            Assert.AreEqual(2, dictionary[1].Count);
            Assert.AreEqual("1", dictionary[1].ElementAt(0));
            Assert.AreEqual("2", dictionary[1].ElementAt(1));
        }

        [Test]
        public void TestAddSafeRangeDifferentTypes() {
            var dictionary = new Dictionary<int, List<string>>();

            dictionary.AddSafe(1, new List<string> { "1" });
            Assert.AreEqual(1, dictionary[1].Count);
            Assert.AreEqual("1", dictionary[1].ElementAt(0));

            dictionary.AddSafe(1, new List<string> { "1", "2", "3" });
            Assert.AreEqual(3, dictionary[1].Count);
            Assert.AreEqual("1", dictionary[1].ElementAt(0));
            Assert.AreEqual("2", dictionary[1].ElementAt(1));
            Assert.AreEqual("3", dictionary[1].ElementAt(2));
        }

        [Test]
        public void TestAddSafeRangeSameType() {
            var dictionary = new Dictionary<int, List<int>>();

            dictionary.AddSafe(1, new List<int> { 1 });
            Assert.AreEqual(1, dictionary[1].Count);
            Assert.AreEqual(1, dictionary[1].ElementAt(0));

            dictionary.AddSafe(1, new List<int> { 1, 2, 3 });
            Assert.AreEqual(3, dictionary[1].Count);
            Assert.AreEqual(1, dictionary[1].ElementAt(0));
            Assert.AreEqual(2, dictionary[1].ElementAt(1));
            Assert.AreEqual(3, dictionary[1].ElementAt(2));
        }

        [Test]
        public void TestAddSafeSameType() {
            var dictionary = new Dictionary<int, List<int>>();

            dictionary.AddSafe(1, 1);
            Assert.AreEqual(1, dictionary[1].Count);
            Assert.AreEqual(1, dictionary[1].ElementAt(0));

            dictionary.AddSafe(1, 2);
            Assert.AreEqual(2, dictionary[1].Count);
            Assert.AreEqual(1, dictionary[1].ElementAt(0));
            Assert.AreEqual(2, dictionary[1].ElementAt(1));
        }

        [Test]
        public void TestInvertDictionaryDifferentTypes() {
            var dictionary = new Dictionary<int, List<string>>();
            dictionary.AddSafe(1, new List<string> { "1", "2", "3" });
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(3, dictionary[1].Count);
            var inverted = dictionary.InvertDictionary();
            Assert.AreEqual(3, inverted.Count);
            Assert.AreEqual(1, inverted["1"].Count);
            Assert.AreEqual(1, inverted["1"].ElementAt(0));
            Assert.AreEqual(1, inverted["2"].Count);
            Assert.AreEqual(1, inverted["2"].ElementAt(0));
            Assert.AreEqual(1, inverted["3"].Count);
            Assert.AreEqual(1, inverted["3"].ElementAt(0));
        }

        [Test]
        public void TestInvertDictionarySameType() {
            var dictionary = new Dictionary<int, List<int>>();
            dictionary.AddSafe(1, new List<int> { 1, 2, 3 });
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(3, dictionary[1].Count);
            var inverted = dictionary.InvertDictionary();
            Assert.AreEqual(3, inverted.Count);
            Assert.AreEqual(1, inverted[1].Count);
            Assert.AreEqual(1, inverted[1].ElementAt(0));
            Assert.AreEqual(1, inverted[2].Count);
            Assert.AreEqual(1, inverted[2].ElementAt(0));
            Assert.AreEqual(1, inverted[3].Count);
            Assert.AreEqual(1, inverted[3].ElementAt(0));
        }

        [Test]
        public void TestToArrayDictionary() {
            var dictionary = new Dictionary<int, List<int>>();
            dictionary.AddSafe(1, new List<int> { 1, 2, 3 });
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(3, dictionary[1].Count);
            Assert.AreEqual(1, dictionary[1][0]);
            Assert.AreEqual(2, dictionary[1][1]);
            Assert.AreEqual(3, dictionary[1][2]);
        }
    }
}