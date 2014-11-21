using System;
using System.IO;
using System.Net;
using System.Web;

namespace SitecoreORM.Services
{
  public class HttpService 
  {
    private readonly string _domain;
    private readonly string _userName;
    private readonly string _password;

    public HttpService() : this(CurrentDomain, null, null)
    {
    }

    public HttpService(string domain, string userName, string password)
    {
      _domain = domain;
      _userName = userName;
      _password = password;
    }

    public string Domain
    {
      get { return _domain; }
    }

    public byte[] Get(string relativePath, out string contentType)
    {
      Uri fullyQualifiedUrl = GetFullyQualifiedURL(_domain, relativePath);

      try
      {
        Byte[] bytes;
        HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(fullyQualifiedUrl);
        CredentialCache cc = new CredentialCache
                               {
                                 {fullyQualifiedUrl, "Basic", new NetworkCredential(_userName, _password)}
                               };
        webRequest.PreAuthenticate = true;
        webRequest.Credentials = cc;
        webRequest.KeepAlive = false;
        webRequest.ProtocolVersion = HttpVersion.Version10;
        webRequest.ServicePoint.ConnectionLimit = 24;
        webRequest.Headers.Add("UserAgent", "SitecoreORM");
        webRequest.Method = WebRequestMethods.Http.Get;
        using (WebResponse webResponse = webRequest.GetResponse())
        {
          contentType = webResponse.ContentType;
          using (Stream stream = webResponse.GetResponseStream())
          {
            using (MemoryStream memoryStream = new MemoryStream())
            {
              Byte[] buffer = new Byte[0x1000];
              Int32 bytesRead;
              // ReSharper disable PossibleNullReferenceException
              while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                // ReSharper restore PossibleNullReferenceException
              {
                memoryStream.Write(buffer, 0, bytesRead);
              }
              bytes = memoryStream.ToArray();
            }
          }
        }
        return bytes;
      }
      catch (Exception ex)
      {
        throw new Exception("Failed to retrieve data from '" + fullyQualifiedUrl + "': " + ex.Message, ex);
      }
    }

    private Uri GetFullyQualifiedURL(string domain, string relativePath)
    {
      if (domain.EndsWith("/"))
      {
        domain = domain.Remove(domain.Length - 1, 1);
      }
      if (!relativePath.StartsWith("/"))
      {
        relativePath = "/" + relativePath;
      }
      return new Uri(domain + relativePath);
    }

    protected static string CurrentDomain 
    {
      get
      {
        Uri uri = HttpContext.Current.Request.Url;
        return uri.Scheme + "://" + uri.Host;
      }
    }


  }
}