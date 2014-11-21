using System.Text;
using Sitecore.Data.Items;

namespace SitecoreORM.Services
{
  public class TemplateFieldItemCommentService
  {
    public static string GetComment(TemplateFieldItem fieldItem)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("Gets the {0} {1}. ", fieldItem.Type, fieldItem.Name);
      if (!string.IsNullOrEmpty(fieldItem.Title))
        sb.AppendFormat("Title: {0}. ", fieldItem.Title);
      if (!string.IsNullOrEmpty(fieldItem.Description))
        sb.AppendFormat("Description: {0}. ", fieldItem.Description);
      if (fieldItem.Shared)
        sb.AppendFormat("Shared. ");
      if (fieldItem.Unversioned)
        sb.AppendFormat("Unversioned. ");
      if (!string.IsNullOrEmpty(fieldItem.Source))
        sb.AppendFormat("Datasource: {0}", fieldItem.Source);
      return sb.ToString();
    }
  }
}