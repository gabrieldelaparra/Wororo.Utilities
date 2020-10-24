using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wororo.Utilities
{
    public static class XmlSerialization
    {
        public static T DeserializeXml<T>(string inputXmlFilename)
        {
            if (!File.Exists(inputXmlFilename))
                return default;
            using var stream = new FileStream(inputXmlFilename, FileMode.Open, FileAccess.Read);
            if (stream.Length <= 0) return default;
            var s = new XmlSerializer(typeof(T));
            return (T) s.Deserialize(stream);
        }

        public static void SerializeXml(this object objectToSerialize, string outputXmlFilename)
        {
            outputXmlFilename.CreatePathIfNotExists();

            var xmlWriterSettings = new XmlWriterSettings {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            using var stream = new FileStream(outputXmlFilename, FileMode.Create, FileAccess.Write);
            using var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
            var serializer = new XmlSerializer(objectToSerialize.GetType());
            serializer.Serialize(xmlWriter, objectToSerialize);
        }
    }
}