using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
    public class ValueExtensionsTests
    {
        [Test]
        public void TestBoolToInt() {
            const bool bTrue = true;
            const bool bFalse = false;

            Assert.AreEqual(1, bTrue.ToBoolInt());
            Assert.AreEqual(0, bFalse.ToBoolInt());
        }

        [Test]
        public void TestBoolToOneZeroString() {
            const bool bTrue = true;
            const bool bFalse = false;

            Assert.AreEqual("1", bTrue.ToOneOrZeroString());
            Assert.AreEqual("0", bFalse.ToOneOrZeroString());
        }

        [Test]
        public void TestIntObjectArrayToDoubleEnumerable() {
            var expected1 = new List<double> { 1, 0, 2, 3, 4 };
            var expected2 = new List<double> { 1.2, -0.4 };

            var input1 = new object[] { null, 1, 0, 2, 3, 4 };
            var input2 = (object)new object[] { null, 1, 0, 2, 3, 4 };
            var input3 = (object)new object[] { null };
            var input4 = new object[] { null, 1.2, -0.4 };

            var actual1 = input1.ToDoubleEnumerable();
            var actual2 = input2.ToDoubleEnumerable();
            var actual3 = input3.ToDoubleEnumerable();
            var actual4 = input4.ToDoubleEnumerable();

            Assert.AreEqual(expected1.Count, actual1.Count());
            Assert.IsFalse(expected1.Except(actual1).Any());

            Assert.AreEqual(expected1.Count, actual2.Count());
            Assert.IsFalse(expected1.Except(actual2).Any());

            Assert.IsNotNull(actual3);
            Assert.IsFalse(actual3.Any());

            Assert.AreEqual(expected2.Count, actual4.Count());
            Assert.IsFalse(expected2.Except(actual4).Any());
        }

        [Test]
        public void TestIntObjectArrayToIntEnumerable() {
            var expected = new List<int> { 1, 2, 3, 4 };

            var input1 = new object[] { null, 1, 0, 2, 3, 4 };
            var input2 = (object)new object[] { null, 1, 0, 2, 3, 4 };
            var input3 = (object)new object[] { null };

            var actual1 = input1.ToIntEnumerable();
            var actual2 = input2.ToIntEnumerable();
            var actual3 = input3.ToIntEnumerable();

            Assert.AreEqual(expected.Count, actual1.Count());
            Assert.IsFalse(expected.Except(actual1).Any());

            Assert.AreEqual(expected.Count, actual2.Count());
            Assert.IsFalse(expected.Except(actual2).Any());

            Assert.IsNotNull(actual3);
            Assert.IsFalse(actual3.Any());
        }

        [Test]
        public void TestIntObjectArrayToStringEnumerable() {
            var expected = new List<string> { "1", "2", "3", "4" };

            var input1 = new object[] { null, "1", "2", "3", "4" };
            var input2 = (object)new object[] { null, "1", "2", "3", "4" };
            var input3 = (object)new object[] { null };

            var actual1 = input1.ToStringEnumerable();
            var actual2 = input2.ToStringEnumerable();
            var actual3 = input3.ToStringEnumerable();

            Assert.AreEqual(expected.Count, actual1.Count());
            Assert.IsFalse(expected.Except(actual1).Any());

            Assert.AreEqual(expected.Count, actual2.Count());
            Assert.IsFalse(expected.Except(actual2).Any());

            Assert.IsNotNull(actual3);
            Assert.IsFalse(actual3.Any());
        }

        [Test]
        public void TestIntToBool() {
            const int i1 = 0;
            const int i2 = -21;
            const int i3 = 1;
            const int i4 = 4;

            Assert.IsFalse(i1.ToBool());
            Assert.IsTrue(i2.ToBool());
            Assert.IsTrue(i3.ToBool());
            Assert.IsTrue(i4.ToBool());
        }

        [Test]
        public void TestIsEvenIsOdd() {
            const int i1 = 0;
            const int i2 = -21;
            const int i3 = 1;
            const int i4 = 4;
            const int i5 = 73;

            Assert.IsTrue(i1.IsEven());
            Assert.IsTrue(i2.IsOdd());
            Assert.IsTrue(i3.IsOdd());
            Assert.IsTrue(i4.IsEven());
            Assert.IsTrue(i5.IsOdd());

            Assert.IsFalse(i1.IsOdd());
            Assert.IsFalse(i2.IsEven());
            Assert.IsFalse(i3.IsEven());
            Assert.IsFalse(i4.IsOdd());
            Assert.IsFalse(i5.IsEven());
        }

        [Test]
        public void TestStringToBool() {
            const string i1 = "0";
            const string i2 = "-21";
            const string i3 = "1";
            const string i4 = "4";
            const string i5 = "true";
            const string i6 = "false";
            const string i7 = "other";
            const string i8 = "";
            const string i9 = null;

            Assert.IsFalse(i1.ToBool());
            Assert.IsTrue(i2.ToBool());
            Assert.IsTrue(i3.ToBool());
            Assert.IsTrue(i4.ToBool());

            Assert.IsTrue(i5.ToBool());
            Assert.IsFalse(i6.ToBool());
            Assert.IsFalse(i7.ToBool());
            Assert.IsFalse(i8.ToBool());
            Assert.IsFalse(i9.ToBool());
        }

        [Test]
        public void TestToDoubleMix() {
            const string sample = "a1a1";
            const double expected = 11;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToDoubleNegativeExponential() {
            const string sample = "1234E-05";
            const double expected = 1234E-05;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToDoubleNegativeNumbers() {
            const string sample = "-1234";
            const double expected = -1234;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToDoubleOnlyLetters() {
            const string sample = "abcs";
            const double expected = 0;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToDoubleOnlyNumbers() {
            const string sample = "1234";
            const double expected = 1234;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToDoublePositiveExponential() {
            const string sample = "1234E05";
            const double expected = 1234E05;
            Assert.AreEqual(expected, sample.ToDouble());
        }

        [Test]
        public void TestToIntMix() {
            const string sample = "a1a1";
            const int expected = 11;
            Assert.AreEqual(expected, sample.ToInt());
        }

        [Test]
        public void TestToIntNegativeNumbers() {
            const string sample = "-1234";
            const int expected = -1234;
            Assert.AreEqual(expected, sample.ToInt());
        }

        [Test]
        public void TestToIntOnlyLetters() {
            const string sample = "abcs";
            const int expected = 0;
            Assert.AreEqual(expected, sample.ToInt());
        }

        [Test]
        public void TestToIntOnlyNumbers() {
            const string sample = "1234";
            const int expected = 1234;
            Assert.AreEqual(expected, sample.ToInt());
        }

        [Test]
        public void TestToThreeDecimals() {
            const double d1 = 0.1234;
            const double d2 = 1234.45678;
            const double d3 = 3.2;

            Assert.AreEqual(0.123, d1.ToThreeDecimals());
            Assert.AreEqual(1234.456, d2.ToThreeDecimals());
            Assert.AreEqual(3.2, d3.ToThreeDecimals());

            Assert.IsTrue(0.123.Equals3DigitPrecision(d1.ToThreeDecimals()));
            Assert.IsTrue(1234.456.Equals3DigitPrecision(d2.ToThreeDecimals()));
            Assert.IsTrue(3.2.Equals3DigitPrecision(d3.ToThreeDecimals()));
        }

        [Test]
        public void TestToThreeDecimalsLarger() {
            const double dec = 1.23456789;
            const double expected = 1.234;
            Assert.AreEqual(expected, dec.ToThreeDecimals());
        }

        [Test]
        public void TestToThreeDecimalsShorter() {
            const double dec = 1.2;
            const double expected = 1.200;
            Assert.AreEqual(expected, dec.ToThreeDecimals());
        }

        [Test]
        public void TestToThreeDecimalsZero() {
            const double dec = 0.0;
            const double expected = 0.000;
            Assert.AreEqual(expected, dec.ToThreeDecimals());
        }
    }
}