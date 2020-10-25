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

        }
    }
}
