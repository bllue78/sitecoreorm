using System.Xml;

namespace SitecoreORM.Configuration
{
  public class IncludeTemplate
  {
    private readonly string _templateRoot = string.Empty;
    private readonly string _fileDestinationRoot = string.Empty;
    
    public IncludeTemplate(XmlNode node)
    {
      _templateRoot = node.InnerText;
      if (node.Attributes != null &&
          (node.Attributes["fileDestination"] != null && node.Attributes["fileDestination"].Value != null))
      {
        _fileDestinationRoot = node.Attributes["fileDestination"].Value;
        if (!_fileDestinationRoot.EndsWith("\\"))
          _fileDestinationRoot += "\\";
      }
    }

    public string TemplateRoot
    {
      get
      {
        if (_templateRoot.ToLowerInvariant().StartsWith("/sitecore/templates/"))
          return _templateRoot;
        if (_templateRoot.StartsWith("/"))
          return "/sitecore/templates" + _templateRoot;
        return "/sitecore/templates/" + _templateRoot;
      }
    }

    public string FileDestinationRoot 
    {
      get
      {
        if (!string.IsNullOrEmpty(_fileDestinationRoot))
          return _fileDestinationRoot;
        return ConfigurationRepository.Settings.DefaultFileDestination;
      }
    }
  }
}