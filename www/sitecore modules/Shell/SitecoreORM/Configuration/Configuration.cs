using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore.Data.Items;

namespace SitecoreORM.Configuration
{
  public class Configuration : IConfiguration
  {
    public string ClassNameSuffix
    {
      get; set; 
    }

    public string NameSpacePrefix
    {
      get; set;
    }

    public string NameSpaceRemove
    {
      get; set;
    }

    public string DefaultFileDestination
    {
      get; set;
    }

    public IEnumerable<string> IgnoreTemplateRoots
    {
      get { return _ignoreTemplates; }
    }

    public IEnumerable<IncludeTemplate> IncludeTemplateRoots
    {
      get { return _includeTemplates; }
    }

    public bool TemplateShouldBeIgnored(TemplateItem templateItem)
    {
      return ConfigurationRepository.Settings.IgnoreTemplateRoots.Any(s => templateItem.FullName.ToLowerInvariant().StartsWith(s.ToLowerInvariant()));
    }

    protected void AddIgnoreTemplateRoots(XmlNode node)
    {
      _ignoreTemplates.Add(node.InnerText);
    }

    protected void AddIncludeTemplateRoots(XmlNode node)
    {
      _includeTemplates.Add(new IncludeTemplate(node));
    }

    private readonly List<string> _ignoreTemplates = new List<string>();
    private readonly List<IncludeTemplate> _includeTemplates = new List<IncludeTemplate>();

  }
}