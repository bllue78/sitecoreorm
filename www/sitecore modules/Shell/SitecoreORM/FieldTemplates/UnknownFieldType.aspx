<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="SitecoreORM.CodeProviders" %>
<%@ Import Namespace="SitecoreORM.Services" %>
<script runat="server">
  
  void Page_Load(object sender, System.EventArgs e)
  {
    DataBind();
  }

  private TemplateFieldItem Field
  {
    get { return GenerateFieldProperties.Field; }
  }

  private string PropertyName
  {
    get { return NameNormalizerService.GetValidName(Field.Name); }
  }

  
</script> 

    /// <remarks>
    /// There is no specific template for the field type <%# Field.Type %>. This is a fallback field type template. 
    /// </remarks>
    /// <summary>
    /// <%# TemplateFieldItemCommentService.GetComment(Field) %>
    /// </summary>
    public string <%# PropertyName %>
    {
      get { return InnerItem["<%# Field.Name %>"]; }
    }
