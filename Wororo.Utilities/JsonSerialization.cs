using System.IO;
using Newtonsoft.Json;

namespace Wororo.Utilities
{
    public static class JsonSerialization
    {
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
