using System.IO;
using Newtonsoft.Json;

namespace Wororo.Utilities
{
    /// <summary>
    ///     Provides methods for serializing and deserializing JSON files.
    /// </summary>
    public static class JsonSerialization
    {
        /// <summary>
        ///     Deserializes a JSON file into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to.</typeparam>
        /// <param name="inputJsonFilename">The path to the input JSON file.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T DeserializeJson<T>(string inputJsonFilename)
        {
            if (inputJsonFilename.IsEmpty()) {
                return default;
            }

            if (!File.Exists(inputJsonFilename)) {
                return default;
            }

            var serializer = new JsonSerializer {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            using var sw = new StreamReader(inputJsonFilename);
            using JsonReader writer = new JsonTextReader(sw);
            return serializer.Deserialize<T>(writer);
        }

        /// <summary>
        ///     Serializes an object to a JSON file.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <param name="outputJsonFilename">The path to the output JSON file.</param>
        public static void SerializeJson(this object objectToSerialize, string outputJsonFilename)
        {
            if (outputJsonFilename.IsEmpty()) {
                return;
            }

            outputJsonFilename.CreatePathIfNotExists();

            var serializer = new JsonSerializer {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };

            using var sw = new StreamWriter(outputJsonFilename);
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, objectToSerialize);
        }
    }
}
