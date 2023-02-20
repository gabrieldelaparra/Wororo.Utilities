using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
    public class StringExtensionsUnitTest
    {
        [Test]
        public void TestIsEmpty()
        {
            string nullString = null;
            const string blankString = "";
            const string spaceString = " ";
            const string doubleSpaceString = "  ";
            const string notBlankString = "string";

            Assert.IsTrue(nullString.IsEmpty());
            Assert.IsTrue(blankString.IsEmpty());
            Assert.IsTrue(spaceString.IsEmpty());
            Assert.IsTrue(doubleSpaceString.IsEmpty());
            Assert.IsFalse(notBlankString.IsEmpty());
        }

        [Test]
        public void TestRemove2PlusSpace()
        {
            const string spaceString = " ";
            const string singleLetter = "a";
            const string doubleSpaceStringStart = "  hello world";
            const string doubleSpaceStringMid = "hello  world";
            const string doubleSpaceStringEnd = "hello world  ";
            const string tripleSpaceStringStart = "   hello world";
            const string tripleSpaceStringMid = "hello   world";
            const string tripleSpaceStringEnd = "hello world   ";
            const string fourSpaceStringMid = "hello    world";
            const string sevenSpaceStringMid = "hello    world";
            const string twoDoubleSpaceMid = "hello  world  of  ours";

            const string helloWorld = "hello world";
            Assert.AreEqual(string.Empty, spaceString.Remove2PlusSpaces());
            Assert.AreEqual("a", singleLetter.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, doubleSpaceStringStart.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, doubleSpaceStringMid.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, doubleSpaceStringEnd.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, tripleSpaceStringStart.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, tripleSpaceStringMid.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, tripleSpaceStringEnd.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, fourSpaceStringMid.Remove2PlusSpaces());
            Assert.AreEqual(helloWorld, sevenSpaceStringMid.Remove2PlusSpaces());
            Assert.AreEqual("hello world of ours", twoDoubleSpaceMid.Remove2PlusSpaces());
        }

        [Test]
        public void TestRemoveSymbols()
        {
            const string helloWorld = "hello world";

            var withSymbols1 = $".-_!+*ç)(/%(&=!è$ä^'§<>{helloWorld}.-_!+*ç)(/%(&=!è$ä^'§<>";
            var withSymbols2 = ".-_!+*ç)(/%(&=!è$ä^'§<>hello.-_!+*ç)(/%(&=!è$ä^'§<> world.-_!+*ç)(/%(&=!è$ä^'§<>";
            Assert.AreEqual(helloWorld, withSymbols1.RemoveSymbols());
            Assert.AreEqual(helloWorld, withSymbols2.RemoveSymbols());
        }

        [Test]
        public void TestTabToSpaces()
        {
            const string helloTabWorld = "hello\tworld";
            const string helloTabTabWorld = "hello\t\tworld";

            Assert.AreEqual("hello world", helloTabWorld.TabToSpaces());
            Assert.AreEqual("hello  world", helloTabTabWorld.TabToSpaces());
        }

        [Test]
        public void TestToDigitsOnly()
        {
            const string string1 = "hello 3 world";
            const string string2 = "hello 34 world 45";
            const string string3 = "hello -34 world .45";

            Assert.AreEqual("3", string1.ToDigitsOnly());
            Assert.AreEqual("3445", string2.ToDigitsOnly());
            Assert.AreEqual("3445", string3.ToDigitsOnly());
        }

        [Test]
        public void TestToDouble()
        {
            const string string1 = "3";
            const string string2 = "3445";
            const string string3 = "-34.45";

            Assert.AreEqual(3, string1.ToDouble());
            Assert.AreEqual(3445, string2.ToDouble());
            Assert.AreEqual(-34.45, string3.ToDouble());
        }

        [Test]
        public void TestToInt()
        {
            const string string1 = "3";
            const string string2 = "3445";
            const string string3 = "-34.45";

            Assert.AreEqual(3, string1.ToInt());
            Assert.AreEqual(3445, string2.ToInt());
            Assert.AreEqual(-34, string3.ToInt());

            const double double1 = -34.45;
            const double double2 = 34.45;

            Assert.AreEqual(-34, double1.ToInt());
            Assert.AreEqual(34, double2.ToInt());
        }

        [Test]
        public void TestToLetters()
        {
            const string string1 = "hello 3 World";
            const string string2 = "hello 34 World 45";
            const string string3 = "hello -34 World .45";

            Assert.AreEqual("helloWorld", string1.ToLetters());
            Assert.AreEqual("helloWorld", string2.ToLetters());
            Assert.AreEqual("helloWorld", string3.ToLetters());
        }

        [Test]
        public void TestAnyIsLetters()
        {
            const string string1 = "hello 3 World";
            const string string2 = "hello World";
            const string string3 = "-34.45";

            Assert.True(string1.AnyIsLetter());
            Assert.True(string2.AnyIsLetter());
            Assert.False(string3.AnyIsLetter());
        }

        [Test]
        public void TestAnyIsNumber()
        {
            const string string1 = "hello 3 World";
            const string string2 = "hello World";
            const string string3 = "-34.45";

            Assert.True(string1.AnyIsNumber());
            Assert.False(string2.AnyIsNumber());
            Assert.True(string3.AnyIsNumber());
        }

        [Test]
        public void TestToNormalized()
        {
            const string string1 = "hello World á é í ó ú è à é ö ä ü ñ";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                Assert.AreEqual("hello World a e i o u e a e o a u n", string1.ToNormalized());
            }
        }

        [Test]
        public void TestToSentenceAndTitleCase()
        {
            const string lowerCase = "this is a lower case sentence";
            const string sentenceCase = "This is a lower case sentence";
            const string titleCase = "This Is A Lower Case Sentence";
            Assert.AreEqual(sentenceCase, lowerCase.ToSentenceCase());
            Assert.AreEqual(titleCase, lowerCase.ToTitleCase());
        }

        [Test]
        public void TestToSingleLineText()
        {
            const string string1 = "line1\nline2";
            const string string2 = "line1\r\nline2";
            const string string3 = "line1\n\rline2";
            const string string4 = "line1\n\rline2";
            const string string5 = "line1\nline2\nline3";
            var string6 = $"line1{Environment.NewLine}line2{Environment.NewLine}line3";

            Assert.AreEqual("line1 line2", string1.ToSingleLineText());
            Assert.AreEqual("line1 line2", string2.ToSingleLineText());
            Assert.AreEqual("line1 line2", string3.ToSingleLineText());
            Assert.AreEqual("line1 line2", string4.ToSingleLineText());
            Assert.AreEqual("line1 line2 line3", string5.ToSingleLineText());
            Assert.AreEqual("line1 line2 line3", string6.ToSingleLineText());
        }
    }
}
