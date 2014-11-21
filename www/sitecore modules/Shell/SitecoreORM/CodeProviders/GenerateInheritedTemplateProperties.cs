using System;
using System.Text;
using Sitecore.Data;
using SitecoreORM.Configuration;
using SitecoreORM.Services;
using Sitecore.Data.Items;

namespace SitecoreORM.CodeProviders
{
  public class GenerateInheritedTemplateProperties : ICodeProvider
  {
    public static TemplateItem InheritedTemplate 
    {
      get 
      {
        ID inheritedTemplateID = new ID(System.Web.HttpContext.Current.Request.QueryString[_TEMPLATE_QUERYSTRING]);
        return Sitecore.Context.ContentDatabase.GetItem(inheritedTemplateID);
      }
    }

    public string Get(TemplateItem template, params string[] parameters)
    {
      StringBuilder sb = new StringBuilder();

      if (parameters == null || string.IsNullOrEmpty(parameters[0]))
      {
        sb.AppendLine("      // GenerateInheritedTemplateProperties failed: First parameter must contain the relative path and template file name for the inherited templates");
        sb.AppendLine("      // This is an configuration error in CodeTemplate.aspx");
        return sb.ToString();
      }

      foreach (TemplateItem inheritedTemplate in template.BaseTemplates)
      {
        AppendInheritedTemplate(parameters[0], inheritedTemplate, sb);
      }

      return sb.ToString();
    }

    private void AppendInheritedTemplate(string filePathAndName, TemplateItem inheritedTemplate, StringBuilder sb)
    {
      try
      {
        if (ConfigurationRepository.Settings.TemplateShouldBeIgnored(inheritedTemplate))
          return;
        string contentType;
        string fileContents = Encoding.UTF8.GetString(_service.Get(filePathAndName + "?" + _TEMPLATE_QUERYSTRING + "=" + inheritedTemplate.ID, out contentType));
        sb.AppendLine(fileContents);
      }
      catch (Exception ex)
      {
        sb.AppendLine("      // Failed to append inherited template " + inheritedTemplate.FullName + ": " + ex.Message);
      }
    }

    private readonly HttpService _service = new HttpService();
    private const string _TEMPLATE_QUERYSTRING = "inheritedtemplate";

  }
}