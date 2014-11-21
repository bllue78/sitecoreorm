using System;
using System.Reflection;
using SitecoreORM;
using SitecoreORM.Repositories;
using SitecoreORM.Services;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Reflection;

namespace SitecoreORM
{
  public partial class CodeTemplate : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      TemplateRepository rep = new TemplateRepository();
      Template = rep.Get(Factory.GetDatabase("master"), Request.QueryString["id"]);
      DataBind();
    }

    public TemplateItem Template { get; private set; }

    public string Namespace
    {
      get { return NamespaceService.GetNamespace(Template.FullName); }
    }

    public string ClassName
    {
      get { return NameNormalizerService.GetValidClassName(Template.Name); }
    }

    public string ExecuteCodeProvider(string assemblyAndClass, params string[] parameters)
    {
      try
      {
        ICodeProvider provider = ReflectionUtil.CreateObject(assemblyAndClass) as ICodeProvider;
        return provider.Get(Template, parameters);
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }
    }

  }

}