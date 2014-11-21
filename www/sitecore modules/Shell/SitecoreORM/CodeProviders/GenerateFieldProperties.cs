using System;
using System.Text;
using SitecoreORM.Services;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace SitecoreORM.CodeProviders
{
  public class GenerateFieldProperties : ICodeProvider
  {
    public static TemplateFieldItem Field 
    {
      get
      {
        ID fieldID = new ID(System.Web.HttpContext.Current.Request.QueryString[_FIELD_QUERYSTRING]);
        return Sitecore.Context.ContentDatabase.GetItem(fieldID);
      }
    }

    public string Get(TemplateItem template, params string[] parameters)
    {
      StringBuilder sb = new StringBuilder();

      if (parameters == null || string.IsNullOrEmpty(parameters[0]))
      {
        sb.AppendLine("      // GenerateFieldProperties failed: First parameter must contain the relative path to the folder where the field templates are stored");
        sb.AppendLine("      // This is an configuration error in CodeTemplate.aspx");
        return sb.ToString();
      }

      foreach (TemplateFieldItem field in template.OwnFields)
      {
        AppendField(parameters[0], field, sb);
      }
      return sb.ToString();
    }

    private void AppendField(string filePath, TemplateFieldItem field, StringBuilder sb)
    {
      try
      {
        string fieldType = GetFieldTypeName(field);
        string fileName = GetFieldTemplateFilePath(filePath, fieldType);
        string contentType;
        string fileContents = Encoding.UTF8.GetString(_service.Get(fileName + "?" + _FIELD_QUERYSTRING + "=" + field.ID, out contentType));
        sb.AppendLine(fileContents);
      }
      catch (Exception ex)
      {
        sb.AppendLine("      // Feiled to map: " + field.Name + ": " + ex.Message);
      }
    }

    private string GetFieldTypeName(TemplateFieldItem field)
    {
      FieldType fieldType = FieldTypeManager.GetFieldType(field.Type);
      if (fieldType != null)
        return fieldType.Type.Name;

      Item fieldTypeItem = FieldTypeManager.GetFieldTypeItem(field.Type);
      if (fieldTypeItem != null)
        return fieldTypeItem.Name;

      return _UNKNOWN_FIELD_TYPE;
    }

    private string GetFieldTemplateFilePath(string path, string fieldType)
    {
      string fileName = path + fieldType + ".aspx";
      if (FileService.FileExists(fileName))
        return fileName;
      return path + _UNKNOWN_FIELD_TYPE + ".aspx";
    }

    private readonly HttpService _service = new HttpService();
    private const string _UNKNOWN_FIELD_TYPE = "UnknownFieldType";
    private const string _FIELD_QUERYSTRING = "field";
  }
}