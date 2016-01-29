using BookStore.Data.Model;
using BookStore.Data.Repositories;
using BookStore.Data.SharePoint.Client;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.Data.SharePoint.Client.Repositories
{
    public class SharePointClientCategoriesRepository : ICategoriesRepository
    {
        private const string ListName = "Book Categories";

        public List<Category> GetAll()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
            var clientContext = spContext.CreateUserClientContextForSPHost();

            var web = clientContext.Web;
            var categoriesList = web.Lists.GetByTitle(ListName);
            var items = categoriesList.GetItems(new CamlQuery());

            clientContext.Load(items);
            clientContext.ExecuteQueryRetry();

            var categories = items.OfType<ListItem>().Select(it => new Category
            {
                Id = it.Id,
                Title = it["Title"] as string
            }).ToList();

            return categories;
        }
    }
}
