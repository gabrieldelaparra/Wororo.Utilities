using System;
using System.Collections.Generic;
using System.Linq;

namespace Wororo.Utilities
{
    public static class ValueExtensions
    {
        //TODO: I am not sure how this can be of any good
        //public static bool Equal(this double double1, double double2)
        //{
        //    return Math.Abs(double1).Equals(Math.Abs(double2));
        //}

        public static bool Equals3DigitPrecision(this double left, double right)
        {
            return left.ToThreeDecimals().Equals(right.ToThreeDecimals());
        }

        public static bool IsEven(this int i)
        {
            return i % 2 == 0;
        }

        public static bool IsOdd(this int i)
        {
            return !i.IsEven();
        }

        public static bool ToBool(this int input)
        {
            return Convert.ToBoolean(input);
        }

        public static bool ToBool(this string input)
        {
            //No input, let us return false;
            if (input.IsEmpty()) return false;

            //It may be a "1" / "0" string:
            //A "-1" string will be translated (ToNumbers()) to "1"
            //ToNumbers() != ToInt()
            //If not a number, ToNumbers will be -1
            if (input.ToNumbers() >= 0) {
                return int.TryParse(input, out var result)
                           ? Convert.ToBoolean(result)
                           : Convert.ToBoolean(input);
            }

            // It may be a "TRUE" / "FALSE" string
            return bool.TryParse(input, out var boolResult) && Convert.ToBoolean(boolResult);
        }

        public static int ToBoolInt(this bool boolean)
        {
            return Convert.ToInt32(boolean);
        }

        public static IEnumerable<double> ToDoubleEnumerable(this object array)
        {
            return ((object[])array).Where(x => x != null).Select(x => Convert.ToDouble(x));
        }

        //TODO: Do I have to filter the 0 values? Sometimes, sometimes not (X, Y, Z for example not, but they are doubles. There could be a case though).
        public static IEnumerable<int> ToIntEnumerable(this object ids)
        {
            return ((object[])ids)?.Where(x => x != null).Select(x => (int)x).Where(x => x > 0) ??
                   Enumerable.Empty<int>();
        }

        public static string ToOneOrZeroString(this bool boolean)
        {
            return boolean ? "1" : "0";
        }

        public static IEnumerable<string> ToStringEnumerable(this object ids)
        {
            return ((object[])ids)?.Where(x => x != null).Select(x => x.ToString())
                                  .Where(x => !string.IsNullOrWhiteSpace(x)) ?? Enumerable.Empty<string>();
        }

        public static double ToThreeDecimals(this double input)
        {
            return Math.Truncate(input * 1000) / 1000;
        }

        /// <summary>
        ///     This is mostly specific to the E3 data. It will use a constructor that takes and int as argument.
        /// </summary>
        /// <typeparam name="T">Type of E3 object class</typeparam>
        /// <param name="array">An array of ints, but they are given as objects.</param>
        /// <returns></returns>
        public static List<T> ToTypeList<T>(this object array)
        {
            return array is IEnumerable<int> intEnumerable
                       ? intEnumerable.Select(x => (T)Activator.CreateInstance(typeof(T), x)).ToList()
                       : array.ToIntEnumerable().Select(x => (T)Activator.CreateInstance(typeof(T), x)).ToList();
        }
    }
}
