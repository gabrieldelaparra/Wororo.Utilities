using System;
using System.ComponentModel;
using System.Linq;

namespace Wororo.Utilities
{
    public static class EnumExtensions
    {
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
