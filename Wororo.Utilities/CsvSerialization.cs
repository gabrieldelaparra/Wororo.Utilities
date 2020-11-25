using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wororo.Utilities
{
    public static class CsvSerialization
    {
        public static T DeserializeCsv<T>(string inputJsonFilename)
        {
            if (inputJsonFilename.IsEmpty())
                return default;
            if (!File.Exists(inputJsonFilename))
                return default;
            //var text = File.ReadAllText(inputJsonFilename);
            //return JsonConvert.DeserializeObject<T>(text);
            return default;
        }

        public static void SerializeCsv(this object objectToSerialize, string outputJsonFilename)
        {
            if (outputJsonFilename.IsEmpty())
                return;

            outputJsonFilename.CreatePathIfNotExists();

            //var serializer = new JsonSerializer
            //        { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

            //using var sw = new StreamWriter(outputJsonFilename);
            //using JsonWriter writer = new JsonTextWriter(sw);
            //serializer.Serialize(writer, objectToSerialize);
        }
    }
}
