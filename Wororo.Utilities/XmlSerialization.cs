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
      if (!File.Exists(inputXmlFilename)) {
        return default;
      }

      using var stream = new FileStream(inputXmlFilename, FileMode.Open, FileAccess.Read);
      if (stream.Length <= 0) return default;
      var s = new XmlSerializer(typeof(T));
      return (T)s.Deserialize(stream);
    }

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
          var attributes = prop.GetCustomAttributes(typeof(System.ComponentModel.DefaultValueAttribute), true);
          if (attributes.Length > 0) {
            var defaultValueAttributeProp = new XmlAttributes();
            defaultValueAttributeProp.XmlDefaultValue = ((System.ComponentModel.DefaultValueAttribute)attributes[0]).Value;
            defaultValueAttribute.Add(type, prop.Name, defaultValueAttributeProp);
          }
        }
        serializer = new XmlSerializer(objectToSerialize.GetType(), defaultValueAttribute);
      }
      serializer.Serialize(xmlWriter, objectToSerialize, ns);
    }
  }
}