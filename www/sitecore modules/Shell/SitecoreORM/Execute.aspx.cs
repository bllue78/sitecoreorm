using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using SitecoreORM.Configuration;
using SitecoreORM.Repositories;
using SitecoreORM.Services;

namespace SitecoreORM
{
  public partial class Execute : System.Web.UI.Page
  {
    private Database _database = Factory.GetDatabase("master");
    private HttpService _httpService;
 
    protected void Page_Load(object sender, EventArgs e)
    {
      _httpService = new HttpService();

      WriteResponse("SitecoreORM started");
      WriteResponse("Date: {0}", DateTime.Now.ToUniversalTime());
      WriteResponse("Server: {0}", _httpService.Domain);
      WriteResponse("<hr/>");

      foreach (IncludeTemplate includeTemplate in ConfigurationRepository.Settings.IncludeTemplateRoots)
      {
        Generate(includeTemplate);
      }

      WriteResponse("<hr/>");
      WriteResponse("SitecoreORM Finished: {0}", DateTime.Now.ToUniversalTime());
    }

    protected void Generate(IncludeTemplate includeTemplate)
    {
      Item root = _database.GetItem(includeTemplate.TemplateRoot);
      TemplateRepository rep = new TemplateRepository();
      foreach (TemplateItem template in rep.Get(root, true))
      {
        if (ConfigurationRepository.Settings.TemplateShouldBeIgnored(template))
        {
          WriteResponse("<font color=orange><b>Ignoring {0}</b> ({1})</font>", NameNormalizerService.GetValidName(template.Name), template.FullName);
          continue;
        }
        WriteResponse("<b>{0}</b> ({1})",NameNormalizerService.GetValidName(template.Name),template.FullName);
        string contentType;
        byte[] result = _httpService.Get("/sitecore modules/shell/sitecoreorm/codetemplate.aspx?id=" + template.ID, out contentType);
        FileService.Save(template, includeTemplate.FileDestinationRoot, Encoding.UTF8.GetString(result));
      }
    }

    private void WriteResponse(string response, params object[] args)
    {
      Response.Write(string.Format(response, args) + "<br/>");
    }
  }
}