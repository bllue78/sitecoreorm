using System.Xml;

namespace SitecoreORM.Configuration
{
  public class ConfigurationRepository
  {
    private static IConfiguration _config;
    private const string _PATH = "SitecoreORM/ORMConfiguration";

    public static IConfiguration Settings
    {
      get
      {
        if (_config == null)
        {
          XmlNode xmlNode = Sitecore.Configuration.Factory.GetConfigNode(_PATH);
          Sitecore.Diagnostics.Assert.IsNotNull(xmlNode, _PATH);
          _config = Sitecore.Configuration.Factory.CreateObject<IConfiguration>(xmlNode);
        }
        return _config;
      }
    }
  }
}