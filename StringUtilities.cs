using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wororo.Utilities
{
    public static class StringUtilities
    {
        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);
        public static string ToSpacedCSV(this IEnumerable<string> values) => string.Join(", ", values);
        public static string FirstDotSegment(this string productName)
        {
            return productName.Contains(".") ? productName.Split('.').FirstOrDefault() : productName;
        }
        public static string ToLetters(this string text)
        {
            return Regex.Replace(text, @"[^a-zA-Z]", string.Empty);
        }

        public static string ToNormalized(this string text) =>
                new string(text.Normalize(NormalizationForm.FormD)
                                        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                                        .ToArray())
                        .Normalize(NormalizationForm.FormC);

        public static int ToNumbers(this string text)
        {
            var tryInt = -1;

            if (string.IsNullOrWhiteSpace(text)) return tryInt;

            var replaced = Regex.Replace(text, @"[\D]", string.Empty);
            if (!string.IsNullOrWhiteSpace(replaced))
                int.TryParse(replaced, out tryInt);
            return tryInt;
        }

        //public static string ToNullIfEmpty(this string text) {
        //    return text.IsEmpty() ? null : text;
        //}
        public static string ToSingleLineText(this string text) {
            return text.IsEmpty() ? string.Empty : text.Replace(Environment.NewLine," ").Replace(@"\r", " ").Replace(@"\n", " ").TabToSpaces().RemoveSpaces();
        }

        public static string RemoveSymbols(this string text)
        {
            return Regex.Replace(text,"[^0-9a-zA-Z]", " ").RemoveSpaces();
        }

        public static string RemoveSpaces(this string text)
        {
            return Regex.Replace(text, @"[ ]{2,}", " ").Trim();
        }

        public static string TabToSpaces(this string text) {
            return text.Replace('\t', ' ');
        }

        public static string ToSentenceCase(this string lowerCaseString)
        {
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            return r.Replace(lowerCaseString, s => s.Value.ToUpper());
        }

        public static string ToTitleCase(this string lowerCaseString)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(lowerCaseString);
        }
    }
}
