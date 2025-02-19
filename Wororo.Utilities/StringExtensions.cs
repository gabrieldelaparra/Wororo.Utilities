using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp;

namespace Wororo.Utilities;

public static class StringExtensions
{
    private const string Space = " ";
    private static readonly Regex RemoveSpacesRegex = new("[ ]{2,}", RegexOptions.Compiled);
    private static readonly Regex RemoveSymbolsRegex = new("[^0-9a-zA-Z\\s]", RegexOptions.Compiled);
    private static readonly Regex ToDigitsOnlyRegex = new("[\\D]", RegexOptions.Compiled);
    private static readonly Regex ToDoubleRegex = new("[^0-9+-.,eE]", RegexOptions.Compiled);
    private static readonly Regex ToIntRegex = new("[^0-9+-.,]", RegexOptions.Compiled);
    private static readonly Regex ToLettersOnlyRegex = new(@"[^a-zA-Z]", RegexOptions.Compiled);
    private static readonly Regex ToSentenceCaseRegex = new(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
    private static readonly Regex ToSingleLineRegex = new(@"[\r\n]+", RegexOptions.Compiled);

    public static bool AnyIsLetter(this string value)
    {
        return value.Any(char.IsLetter);
    }

    public static bool AnyIsNumber(this string value)
    {
        return value.Any(char.IsNumber);
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source?.IndexOf(toCheck, comp) >= 0;
    }

    public static string GetRegexMatchGroup(this string input, string regexPattern, int groupIndex)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var r = new Regex(regexPattern);
        var m = r.Match(input);
        return m.Success ? m.Groups.Count > groupIndex ? m.Groups[groupIndex].Value : string.Empty : string.Empty;
    }

    public static bool IsEmpty(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotEmpty(this string? value)
    {
        return !value.IsEmpty();
    }

    public static string Remove2PlusSpaces(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) {
            return string.Empty;
        }

        return RemoveSpacesRegex.Replace(text, Space).Trim();
    }

    public static string RemoveSymbols(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) {
            return string.Empty;
        }

        return RemoveSymbolsRegex.Replace(text, string.Empty).Remove2PlusSpaces();
    }

    public static string TabToSpaces(this string text)
    {
        return text.Replace('\t', ' ');
    }

    public static string ToDigitsOnly(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return string.Empty;
        }

        return ToDigitsOnlyRegex.Replace(input, string.Empty);
    }

    public static double ToDouble(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return 0;
        }

        var value = ToDoubleRegex.Replace(input, string.Empty);
        return value.IsEmpty() ? 0 : double.Parse(value);
    }

    public static string ToFormatLiteral(this string input, bool addQuote = false)
    {
        return input.IsEmpty()
                   ? string.Empty
                   : SymbolDisplay.FormatLiteral(input, addQuote);
    }

    public static int ToInt(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return 0;
        }

        var value = ToIntRegex.Replace(input, string.Empty);
        return value.IsEmpty() ? 0 : (int)double.Parse(value);
    }

    public static string ToLetters(this string text)
    {
        return ToLettersOnlyRegex.Replace(text, string.Empty);
    }

    public static string ToNormalized(this string text)
    {
        return new string(text.Normalize(NormalizationForm.FormD)
                              .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                              .ToArray())
            .Normalize(NormalizationForm.FormC);
    }

    public static int ToNumbers(this string text)
    {
        var tryInt = -1;
        if (text.IsEmpty()) return tryInt;

        var replaced = ToDigitsOnlyRegex.Replace(text, string.Empty);

        if (!string.IsNullOrWhiteSpace(replaced)) {
            int.TryParse(replaced, out tryInt);
        }

        return tryInt;
    }

    public static string ToSentenceCase(this string lowerCaseString)
    {
        return ToSentenceCaseRegex.Replace(lowerCaseString, s => s.Value.ToUpper());
    }

    public static string ToSingleLineText(this string text)
    {
        return text.IsEmpty()
                   ? string.Empty
                   : ToSingleLineRegex.Replace(text.Replace(Environment.NewLine, Space), Space).Remove2PlusSpaces();
    }

    public static string ToTitleCase(this string lowerCaseString)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lowerCaseString);
    }
}
