using BookStore.Data.Model;
using BookStore.Data.Repositories;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.SharePoint.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private const string ListName = "Book Categories";

        public List<Category> GetAll()
        {
            var web = SPContext.Current.Web;
            var categoriesList = web.Lists[ListName];
            var items = categoriesList.GetItems(new SPQuery());
            var categories = items.OfType<SPListItem>().Select(it => new Category
            {
                Id = it.ID,
                Title = it.Title
            }).ToList();
            return categories;
        }
    }
}
