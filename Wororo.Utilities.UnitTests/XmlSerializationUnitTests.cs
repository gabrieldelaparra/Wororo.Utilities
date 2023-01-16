using NUnit.Framework;

namespace Wororo.Utilities.UnitTests
{
  public class XmlSerializationUnitTests
  {
  
    [Test]
    public void SerializeXml_Deserialize_ValidInput()
    {
      //Arrange
      var person = new MockPerson { Name = "John", Age = 30 };
      //Act
      person.SerializeXml("SerializeXml_ValidInput_ShouldReturnXmlString.xml");
      var result = XmlSerialization.DeserializeXml<MockPerson>("SerializeXml_ValidInput_ShouldReturnXmlString.xml");
      //Assert
      Assert.IsNotNull(result);
      Assert.AreEqual("John", result.Name);
      Assert.AreEqual(30, result.Age);
    }

    public class MockPerson
    {
      public string Name { get; set; }
      public int Age { get; set; }
    }
  }
 
}
