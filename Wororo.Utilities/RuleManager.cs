using System;

namespace Wororo.Utilities
{
    public static class RuleManager
    {
        ///// <summary>
        /////     Gets the rules from a specified file and deserializes them to the specified type.
        ///// </summary>
        ///// <param name="rulesFilename">The name of the rules file.</param>
        ///// <param name="expectedType">The expected type of the deserialized rules.</param>
        ///// <param name="callingNamespace">The namespace of the calling class.</param>
        ///// <returns>The deserialized rules.</returns>
        //public static object GetRules(string rulesFilename, Type expectedType, string callingNamespace)
        //{
        //    //Template rules to be present in its template folder:
        //    string templateFolder = callingNamespace + ".Templates.";

        //    //Default would be the rulesFolder:
        //    string outputFolder = Defaults.RulesFolder;

        //    //Check Template Existence in Rules Folder, if not, copy the template to the rules folder:
        //    FileExtensions.RestoreTemplateFileIfDoesNotExists(System.Reflection.Assembly.GetExecutingAssembly().FullName, templateFolder + rulesFilename, outputFolder + rulesFilename);

        //    //Deserialize:
        //    var rules = XMLSerialization.DeserializeXml<expectedType>(outputFolder + rulesFilename);

        //    return rules;
        //}
    }
}
