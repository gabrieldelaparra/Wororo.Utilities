using System;

namespace Wororo.Utilities
{
    public static class NumbersExtensions
    {
        public static bool IsEven(this int i)
        {
            return i % 2 == 0;
        }

        public static bool IsOdd(this int i)
        {
            return !i.IsEven();
        }

        public static double ToThreeDecimals(this double input)
        {
            return Math.Truncate(input * 1000) / 1000;
        }
    }
}