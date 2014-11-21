using System.IO;
using System.Web;
using Sitecore.Data.Items;

namespace SitecoreORM.Services
{
  public class FileService
  {
    public static string GetContents(string relativePath)
    {
      return File.ReadAllText(GetPhysicalPath(relativePath, false));
    }

    public static bool FileExists(string relativePathAndFileName)
    {
      return File.Exists(GetPhysicalPath(relativePathAndFileName, false));
    }

    public static void Save(TemplateItem template, string rootPath, string fileContents)
    {
      Save(rootPath, NamespaceService.GetNamespace(template.FullName).Replace(".", @"\"),
                     NameNormalizerService.GetValidClassName(template.Name) + ".cs", fileContents);
    }

    private static void Save(string rootPath, string relativePath, string fileName, string fileContents)
    {
      string path = GetPhysicalPath(rootPath, true) + relativePath.Replace(@"\", "/");
      if (!path.EndsWith("/"))
        path += "/";
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      File.WriteAllText(path + fileName, fileContents, System.Text.Encoding.UTF8);
    }

    private static string GetPhysicalPath(string path, bool includeEndingSlash)
    {
      if (path.Contains(@"\"))
        return path;
      string physicalPath = HttpContext.Current.Server.MapPath(path);
      if (!includeEndingSlash)
        return physicalPath;
      if (!physicalPath.EndsWith(@"\"))
        return physicalPath + @"\";
      return physicalPath;
    }

  }
}