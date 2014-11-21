using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SitecoreORM.Repositories
{
  public class TemplateRepository
  {
    private const string _TEMPLATE_KEY = "template";
    private readonly List<TemplateItem> _item = new List<TemplateItem>();

    public TemplateItem Get(Database database, string ID)
    {
      Item item = database.GetItem(ID);
      if (item == null)
        return null;
      if (item.Template.Key != _TEMPLATE_KEY)
        return null;
      return new TemplateItem(item);
    }

    public IEnumerable<TemplateItem> Get(Item root)
    {
      foreach (Item child in root.Children)
      {
        if (child.Template.Key == _TEMPLATE_KEY)
          yield return new TemplateItem(child);
      }
    }

    public IEnumerable<TemplateItem> Get(Item root, bool deep)
    {
      if (!deep)
        return Get(root);
      
      foreach (Item child in root.Children)
      {
        if (child.Template.Key == _TEMPLATE_KEY)
          _item.Add(new TemplateItem(child));
        else
          Get(child, true);
      }
      return _item;
    }


  }
}