using System.IO;
using Newtonsoft.Json;

namespace Wororo.Utilities
{
    public static class JsonSerialization
    {
        public static T DeserializeJson<T>(string inputJsonFilename)
        {
            if (inputJsonFilename.IsEmpty())
                return default;
            if (!File.Exists(inputJsonFilename))
                return default;
            var text = File.ReadAllText(inputJsonFilename);
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static void SerializeJson(this object objectToSerialize, string outputJsonFilename)
        {
            if (outputJsonFilename.IsEmpty())
                return;

            outputJsonFilename.CreatePathIfNotExists();
            var serializer = new JsonSerializer
                {NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented};

            using var sw = new StreamWriter(outputJsonFilename);
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, objectToSerialize);
        }
    }
}
