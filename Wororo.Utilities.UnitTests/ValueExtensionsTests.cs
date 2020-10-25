using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Wororo.Utilities;

namespace Wororo.Utilities.UnitTests
{
    public class ValueExtensionsTests
    {
        [Test]
        public void TestToThreeDecimals()
        {
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
        public void TestIsEvenIsOdd()
        {
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
        public void TestStringToBool()
        {
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
        public void TestBoolToInt()
        {
            const bool bTrue = true;
            const bool bFalse = false;

            Assert.AreEqual(1,bTrue.ToBoolInt());
            Assert.AreEqual(0,bFalse.ToBoolInt());
        }

        [Test]
        public void TestBoolToOneZeroString()
        {
            const bool bTrue = true;
            const bool bFalse = false;

            Assert.AreEqual("1", bTrue.ToOneOrZeroString());
            Assert.AreEqual("0", bFalse.ToOneOrZeroString());
        }
    }
}
