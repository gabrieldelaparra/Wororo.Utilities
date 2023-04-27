using System.IO;

namespace Wororo.Utilities
{
    /// <summary>
    ///     Provides methods for serializing and deserializing CSV files.
    /// </summary>
    public static class CsvSerialization
    {
        ///// <summary>
        /////     Deserializes a CSV file to the specified object type.
        ///// </summary>
        ///// <typeparam name="T">The type of object to deserialize the CSV file to.</typeparam>
        ///// <param name="inputCsvFilename">The path and filename of the CSV file to deserialize.</param>
        ///// <returns>The deserialized object of the specified type.</returns>
        //public static T DeserializeCsv<T>(string inputCsvFilename)
        //{
        //    if (inputCsvFilename.IsEmpty()) {
        //        return default;
        //    }

        //    if (!File.Exists(inputCsvFilename)) {
        //        return default;
        //    }

        //    //var text = File.ReadAllText(inputJsonFilename);
        //    //return JsonConvert.DeserializeObject<T>(text);
        //    return default;
        //}

        ///// <summary>
        /////     Serializes an object to a CSV file.
        ///// </summary>
        ///// <param name="objectToSerialize">The object to serialize to a CSV file.</param>
        ///// <param name="outputCsvFilename">The path and filename of the CSV file to create or overwrite.</param>
        //public static void SerializeCsv(this object objectToSerialize, string outputCsvFilename)
        //{
        //    if (outputCsvFilename.IsEmpty()) {
        //        return;
        //    }

        //    outputCsvFilename.CreatePathIfNotExists();

        //    //var serializer = new JsonSerializer
        //    //        { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

        //    //using var sw = new StreamWriter(outputJsonFilename);
        //    //using JsonWriter writer = new JsonTextWriter(sw);
        //    //serializer.Serialize(writer, objectToSerialize);
        //}
    }
}
