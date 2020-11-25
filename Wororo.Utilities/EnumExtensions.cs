using System;

namespace Wororo.Utilities
{
    public static class EnumExtensions
    {
        public static T Cycle<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) return value;

            var values = (T[])Enum.GetValues(value.GetType());
            var length = values.Length;
            if (length < 2) 
                return value;

            var intValue = Array.IndexOf(values, value);
            return values[++intValue % length];
        }
    }
}
