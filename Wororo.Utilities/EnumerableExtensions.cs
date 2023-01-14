using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NaturalSort.Extension;

namespace Wororo.Utilities
{
  public static class EnumerableExtensions
  {
    public static void AddIfNotAlready<T>(this IList<T> list, T value)
    {
      if (!list.Contains(value)) {
        list.Add(value);
      }
    }

    public static void AddSafe<T>(this IList<T> list, T value)
    {
      if (!list.Contains(value)) list.Add(value);
    }

    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                 Func<TSource, TKey> keySelector)
    {
      //Via MoreEnumerable.DistinctBy
      return MoreEnumerable.DistinctBy(source, keySelector);
    }

    public static bool HasAtLeast<T>(this IEnumerable<T> enumerable, int atLeast)
    {
      return enumerable.Take(atLeast).Count().Equals(atLeast);
    }

    public static bool HasMoreThanOne<T>(this IEnumerable<T> enumerable)
    {
      return enumerable.HasAtLeast(2);
    }

    public static IEnumerable<T> IntersectIfAny<T>(this IEnumerable<T> source, IEnumerable<T> target)
    {
      if (source.Any() && target.Any()) {
        return source.Intersect(target);
      }

      if (source.Any()) {
        return source.Distinct();
      }

      return target.Distinct();
    }

    public static bool IsSingle<T>(this IEnumerable<T> enumerable)
    {
      return enumerable.Take(2).Count().Equals(1);
    }

    public static string Join(this IEnumerable<string> values, string separator)
    {
      return string.Join(separator, values);
    }

    public static IOrderedEnumerable<string> NaturalSort(this IEnumerable<string> list)
    {
      return list.OrderBy(x => x, StringComparer.OrdinalIgnoreCase.WithNaturalSort());
    }

    public static IOrderedEnumerable<T> NaturalSort<T>(this IEnumerable<T> list, Func<T, string> keySelector)
    {
      return list.OrderBy(keySelector, StringComparer.OrdinalIgnoreCase.WithNaturalSort());
    }

    public static (IEnumerable<T> TrueSlice, IEnumerable<T> FalseSlice) SliceBy<T>(this IEnumerable<T> enumerable,
      Predicate<T> predicate)
    {
      //Via MoreEnumerable.Partition
      return enumerable.Partition(x => predicate(x));
    }

    public static (IEnumerable<T> FirstPart, IEnumerable<T> SecondPart) Split<T>(this IEnumerable<T> enumerable,
      int fromZeroTo)
    {
      var first = enumerable.Take(fromZeroTo);
      var skip = enumerable.Skip(fromZeroTo);
      return (first, skip);
    }

    public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int takeCount)
    {
      var r = new Random();
      return source.OrderBy(x => r.NextDouble()).Take(takeCount);
    }

    public static string ToSpacedCSV(this IEnumerable<string> values)
    {
      return values.Join(", ");
    }

    public static string ToTSV(this IEnumerable<string> values)
    {
      return values.Join("\t");
    }
  }
}