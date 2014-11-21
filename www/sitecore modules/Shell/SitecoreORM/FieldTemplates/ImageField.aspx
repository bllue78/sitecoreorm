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

    /// <summary>
    /// <%# TemplateFieldItemCommentService.GetComment(Field) %>
    /// </summary>
    public Sitecore.Data.Fields.ImageField <%# PropertyName %>
    {
      get { return InnerItem.Fields["<%# Field.Name %>"]; }
    }
