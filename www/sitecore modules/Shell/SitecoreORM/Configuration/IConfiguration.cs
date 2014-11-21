using System.Collections.Generic;
using Sitecore.Data.Items;

namespace SitecoreORM.Configuration
{
  public interface IConfiguration
  {
    string ClassNameSuffix { get; }
    string NameSpacePrefix { get; }
    string NameSpaceRemove { get; }
    string DefaultFileDestination { get; }
    IEnumerable<string> IgnoreTemplateRoots { get; }
    IEnumerable<IncludeTemplate> IncludeTemplateRoots { get; }
    bool TemplateShouldBeIgnored(TemplateItem templateItem);
  }
}