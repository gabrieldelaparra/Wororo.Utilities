using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wororo.Utilities;

/// <summary>
///     A static class that provides methods for serializing and deserializing objects to and from XML.
/// </summary>
public static class XmlSerialization
{
    /// <summary>
    ///     Deserializes an XML file to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="inputXmlFilename">The path and filename of the XML file to deserialize.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? DeserializeXml<T>(string inputXmlFilename)
    {
        if (!File.Exists(inputXmlFilename)) {
            return default;
        }

        using var stream = new FileStream(inputXmlFilename, FileMode.Open, FileAccess.Read);
        if (stream.Length <= 0) return default;
        var s = new XmlSerializer(typeof(T));
        return (T)s.Deserialize(stream);
    }

    /// <summary>
    ///     Serializes an object to an XML file.
    /// </summary>
    /// <param name="objectToSerialize">The object to serialize.</param>
    /// <param name="outputXmlFilename">The path and filename of the XML file to create.</param>
    /// <param name="serializeEmptyElements">A boolean indicating whether to serialize empty XML elements.</param>
    /// <param name="serializeDefaultValues">A boolean indicating whether to serialize default values for properties.</param>
    public static void SerializeXml(this object objectToSerialize, string outputXmlFilename, bool serializeEmptyElements = false, bool serializeDefaultValues = false)
    {
        outputXmlFilename.CreatePathIfNotExists();

        var xmlWriterSettings = new XmlWriterSettings {
            Indent = true,
            Encoding = Encoding.UTF8,
            OmitXmlDeclaration = serializeEmptyElements
        };

        using var stream = new FileStream(outputXmlFilename, FileMode.Create, FileAccess.Write);
        using var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings);
        var serializer = new XmlSerializer(objectToSerialize.GetType());
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");

        if (serializeDefaultValues) {
            var defaultValueAttribute = new XmlAttributeOverrides();
            var type = objectToSerialize.GetType();
            var props = type.GetProperties();

            foreach (var prop in props) {
                var attributes = prop.GetCustomAttributes(typeof(DefaultValueAttribute), true);

                if (attributes.Length > 0) {
                    var defaultValueAttributeProp = new XmlAttributes();
                    defaultValueAttributeProp.XmlDefaultValue = ((DefaultValueAttribute)attributes[0]).Value;
                    defaultValueAttribute.Add(type, prop.Name, defaultValueAttributeProp);
                }
            }

            serializer = new XmlSerializer(objectToSerialize.GetType(), defaultValueAttribute);
        }

        serializer.Serialize(xmlWriter, objectToSerialize, ns);
    }
}
