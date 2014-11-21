using System.Text;
using SitecoreORM.Services;
using Sitecore.Data.Items;

namespace SitecoreORM.CodeProviders
{
  public class GenerateFieldNames : ICodeProvider
  {
    public string Get(TemplateItem template, params string[] parameters)
    {
      StringBuilder sb = new StringBuilder();
      foreach (TemplateFieldItem field in template.OwnFields)
      {
        sb.AppendLine(string.Format(@"      public static string {0} = ""{1}"";", NameNormalizerService.GetValidName(field.Name), field.Name));
      }
      return sb.ToString();
    }
  }
}