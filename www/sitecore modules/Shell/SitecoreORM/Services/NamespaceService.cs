using SitecoreORM.Configuration;

namespace SitecoreORM.Services
{
  public class NamespaceService
  {
    /// <summary>
    /// Gets a valid name space from a Sitecore template path or file path
    /// </summary>
    /// <param name="path">Sitecore template path or file path</param>
    /// <returns>String that can be used as a namespace</returns>
    public static string GetNamespace(string path)
    {
      string namespacePrefix = ConfigurationRepository.Settings.NameSpacePrefix; 
      string namespaceRemove = ConfigurationRepository.Settings.NameSpaceRemove;
      if (string.IsNullOrEmpty(namespaceRemove))
        namespaceRemove += " "; 

      string nameSpace = string.Empty;
      string[] parts = path.Split('/');
      for (int i=0;i<parts.Length-1;i++)
        nameSpace += NameNormalizerService.GetValidName(parts[i]) + ".";
      return (namespacePrefix + nameSpace.Replace(namespaceRemove, string.Empty)).Trim('.');
    }

  }
}