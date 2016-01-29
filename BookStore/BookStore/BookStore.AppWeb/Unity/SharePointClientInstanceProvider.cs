using BookStore.Application.Services;
using BookStore.Data.Repositories;
using BookStore.Data.SharePoint.Client.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Unity
{
    public class SharePointClientInstanceProvider
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICategoriesRepository, SharePointClientCategoriesRepository>();
            container.RegisterType<IBooksRepository, SharePointClientBooksRepository>();
            container.RegisterType<IExternalSearchService, WikipediaService>();
        }
    }
}
