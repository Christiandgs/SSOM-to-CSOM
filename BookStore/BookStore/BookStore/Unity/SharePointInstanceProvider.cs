using BookStore.Application.Services;
using BookStore.Data.Repositories;
using BookStore.Data.SharePoint.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Unity
{
    public class SharePointInstanceProvider
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICategoriesRepository, SharePointCategoriesRepository>();
            container.RegisterType<IBooksRepository, SharePointBooksRepository>();
            container.RegisterType<IExternalSearchService, WikipediaService>();
        }
    }
}
