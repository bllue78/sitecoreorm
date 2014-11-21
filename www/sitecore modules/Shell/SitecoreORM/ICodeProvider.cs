using Sitecore.Data.Items;

namespace SitecoreORM
{
  /// <summary>
  /// ICodeProvider is the interface for all code providers.
  /// You must inherit from this interface in order to use the ExecuteCodeProvider in the CodeTemplate.aspx file
  /// </summary>
  public interface ICodeProvider
  {
    string Get(TemplateItem template, params string[] parameters);
  }
}