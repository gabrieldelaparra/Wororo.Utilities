using System.Collections.Generic;
using System.Linq;

namespace Wororo.Utilities;

/// <summary>
///     This class contains extension methods for dictionaries.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    ///     Adds a value to the specified key in the dictionary. If the key already exists, the value is added to the existing list.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to add the value to.</param>
    /// <param name="key">The key to add the value to.</param>
    /// <param name="value">The value to add to the key.</param>
    public static void AddSafe<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, T2 value)
    {
        if (dictionary.TryGetValue(key, out var list)) {
            list.AddSafe(value);
        }
        else {
            dictionary.Add(key, new List<T2> { value });
        }
    }

    /// <summary>
    ///     Adds a collection of values to the specified key in the dictionary. If the key already exists, the collection is added to the existing list.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to add the collection to.</param>
    /// <param name="key">The key to add the collection to.</param>
    /// <param name="values">The collection of values to add to the key.</param>
    public static void AddSafe<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, IEnumerable<T2> values)
    {
        if (dictionary.TryGetValue(key, out var list)) {
            foreach (var value in values) {
                list.Add(value);
            }
        }
        else {
            dictionary.Add(key, values.Distinct().ToList());
        }
    }

    /// <summary>
    ///     Adds a collection of values to the specified key in the dictionary. If the key already exists, the collection is added to the existing HashSet.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to add the collection to.</param>
    /// <param name="key">The key to add the collection to.</param>
    /// <param name="values">The collection of values to add to the key.</param>
    public static void AddSafe<T1, T2>(this IDictionary<T1, HashSet<T2>> dictionary, T1 key, IEnumerable<T2> values)
    {
        if (dictionary.TryGetValue(key, out var hashSet)) {
            foreach (var value in values) {
                hashSet.Add(value);
            }
        }
        else {
            dictionary.Add(key, new HashSet<T2>());
        }
    }

    /// <summary>
    ///     Inverts the dictionary by swapping the keys and values. The resulting dictionary has a list of keys for each value.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to invert.</param>
    /// <returns>A dictionary with the values as keys and a list of keys as values.</returns>
    public static IDictionary<T2, List<T1>> InvertDictionary<T1, T2>(this IDictionary<T1, List<T2>> dictionary)
    {
        var invertedDictionary = new Dictionary<T2, List<T1>>();

        foreach (var type in dictionary) {
            foreach (var property in type.Value) {
                invertedDictionary.AddSafe(property, type.Key);
            }
        }

        invertedDictionary.TrimExcessDeep();
        return invertedDictionary;
    }

    /// <summary>
    ///     Inverts the dictionary by swapping the keys and values. The resulting dictionary has an array of keys for each value.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to invert.</param>
    /// <returns>A dictionary with the values as keys and an array of keys as values.</returns>
    public static Dictionary<T2, T1[]> InvertDictionary<T1, T2>(this IDictionary<T1, T2[]> dictionary)
    {
        var invertedDictionary = new Dictionary<T2, List<T1>>();

        foreach (var type in dictionary) {
            foreach (var property in type.Value) {
                invertedDictionary.AddSafe(property, type.Key);
            }
        }

        return invertedDictionary.ToArrayDictionary();
    }

    /// <summary>
    ///     Converts a dictionary with a list of values to a dictionary with an array of values.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to convert.</param>
    /// <returns>A dictionary with the same keys and an array of values.</returns>
    public static Dictionary<T1, T2[]> ToArrayDictionary<T1, T2>(this Dictionary<T1, List<T2>> dictionary)
    {
        dictionary.TrimExcessDeep();
        return dictionary.ToDictionary(x => x.Key, x => x.Value.ToArray());
    }

    /// <summary>
    ///     Converts a dictionary with a HashSet of values to a dictionary with an array of values.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to convert.</param>
    /// <returns>A dictionary with the same keys and an array of values.</returns>
    public static Dictionary<T1, T2[]> ToArrayDictionary<T1, T2>(this IDictionary<T1, HashSet<T2>> dictionary)
    {
        return dictionary.ToDictionary(x => x.Key, x => x.Value.ToArray());
    }

    /// <summary>
    ///     Trims the capacity of each list in the dictionary to match the number of elements in the list.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to trim.</param>
    public static void TrimExcessDeep<T1, T2>(this Dictionary<T1, List<T2>> dictionary)
    {
        foreach (var pair in dictionary) {
            pair.Value.TrimExcess();
        }
    }
}
