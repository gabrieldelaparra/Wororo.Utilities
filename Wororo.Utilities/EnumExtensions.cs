using System;
using System.ComponentModel;
using System.Linq;

namespace Wororo.Utilities
{
    /// <summary>
    ///     Contains extension methods for enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Gets the description attribute value of an enumeration value.
        /// </summary>
        /// <param name="enumValue">The enumeration value to get the description for.</param>
        /// <returns>The description attribute value of the given enumeration value.</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var genericEnumType = enumValue.GetType();
            var memberInfo = genericEnumType.GetMember(enumValue.ToString());

            if (memberInfo.Length <= 0) {
                return enumValue.ToString();
            }

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Any()
                       ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                       : enumValue.ToString();
        }

        /// <summary>
        ///     Cycles through the possible values of an enumeration, returning the next value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The current value of the enumeration.</param>
        /// <returns>The next value of the enumeration.</returns>
        public static T Cycle<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) return value;

            var values = (T[])Enum.GetValues(value.GetType());
            var length = values.Length;

            if (length < 2) {
                return value;
            }

            var intValue = Array.IndexOf(values, value);
            return values[++intValue % length];
        }

        /// <summary>
        ///     Converts a string to an enumeration value of the specified type.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type to convert the string to.</typeparam>
        /// <param name="value">The string value to convert to an enumeration value.</param>
        /// <returns>The enumeration value corresponding to the given string.</returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            var defaultValue = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);

            if (value.IsEmpty()) {
                return defaultValue;
            }

            return Enum.TryParse(value, true, out TEnum result) ? result : defaultValue;
        }
    }
}
