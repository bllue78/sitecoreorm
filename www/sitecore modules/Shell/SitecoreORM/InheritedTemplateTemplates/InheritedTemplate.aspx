<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="SitecoreORM.CodeProviders" %>
<%@ Import Namespace="SitecoreORM.Services" %>
<script runat="server">
  
  void Page_Load(object sender, System.EventArgs e)
  {
    DataBind();
  }

  private TemplateItem InheritedTemplate
  {
    get { return GenerateInheritedTemplateProperties.InheritedTemplate; }
  }

  private string Namespace
  {
    get { return NamespaceService.GetNamespace(InheritedTemplate.FullName); }
  }

  private string ClassName
  {
    get { return NameNormalizerService.GetValidClassName(InheritedTemplate.Name); }
  }

  private string PropertyName
  {
    get { return NameNormalizerService.GetValidName(InheritedTemplate.Name); }
  }
</script> 

    /// <summary>
    /// Gets the inherited item <%# Namespace %>.<%# ClassName %>
    /// </summary>            
    private <%# Namespace %>.<%# ClassName %> _<%# PropertyName %>;
    public <%# Namespace %>.<%# ClassName %> <%# PropertyName %>
    {
      get 
      { 
        if (_<%# PropertyName %> == null) 
          _<%# PropertyName %> = new <%# Namespace %>.<%# ClassName %>(InnerItem); 
        return _<%# PropertyName %>;
      }
    }
