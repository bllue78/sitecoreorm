using System.CodeDom.Compiler;
using SitecoreORM.Configuration;

namespace SitecoreORM.Services
{
  public class NameNormalizerService
  {
    /// <summary>
    /// Convert string to a valid function or propertyname
    /// </summary>
    /// <param name="name">input string</param>
    /// <returns>String that can be used as function or property name</returns>
    public static string GetValidName(string name)
    {
      System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");
      string ret = regex.Replace(name, "_");
      if (!char.IsLetter(ret, 0)  && !CodeDomProvider.CreateProvider("C#").IsValidIdentifier(ret))
        ret = string.Concat("_", ret);
      return ret;
    }

    /// <summary>
    /// Convert string to valid class name
    /// </summary>
    /// <param name="name">input string</param>
    /// <returns>Valid propertyname suffixed with the class name suffix</returns>
    public static string GetValidClassName(string name)
    {
      string classNameSuffix = ConfigurationRepository.Settings.ClassNameSuffix; 
      return GetValidName(name) + classNameSuffix;
    }

  }
}