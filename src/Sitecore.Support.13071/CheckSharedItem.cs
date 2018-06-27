using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Publishing.Diagnostics;
using Sitecore.XA.Foundation.Multisite.Extensions;
using Sitecore.XA.Foundation.Multisite.Services;
using Sitecore.XA.Foundation.Publication.Enums;
using Sitecore.XA.Foundation.SitecoreExtensions;
using Sitecore.XA.Foundation.SitecoreExtensions.Repositories;


namespace Sitecore.Support.XA.Foundation.Publication.Pipelines.PublishItem
{
  public class CheckSharedItem: Sitecore.XA.Foundation.Publication.Pipelines.PublishItem.CheckSharedItem
  {

    public CheckSharedItem(IDelegatedAreaService delegatedAreaService):base(delegatedAreaService)
    {
    }
    protected override PublicationStatus CheckPublicationStatus(Item currentItem)
    {
      Item item = ServiceLocator.ServiceProvider.GetService<IDatabaseRepository>().GetDatabase("web").GetItem(currentItem.ID, currentItem.Language);
      if (item == null)
      {
        return PublicationStatus.NeverPublished;
      }
      if (((BaseItem)currentItem)[Templates.Statistics.Fields.__Revision] != ((BaseItem)item)[Templates.Statistics.Fields.__Revision])
      {
        return PublicationStatus.Modified;
      }
      return PublicationStatus.Published;
    }
  }
}