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
    public class SharePointBooksRepository : IBooksRepository
    {
        private const string ListName = "Books";

        public List<Book> GetByCategory(int categoryId)
        {
            var web = SPContext.Current.Web;
            var booksList = web.Lists[ListName];

            var query = new SPQuery();
            query.Query = string.Format(
@"<Where>
    <Eq>
        <FieldRef Name='Category' LookupId='TRUE' />
        <Value Type='Lookup'>{0}</Value>
    </Eq>
</Where>", categoryId);

            var items = booksList.GetItems(query);

            var booksInCategory = items.OfType<SPListItem>().Select(it => new Book
            {
                Id = it.ID,
                Title = it.Title,
                Description = it["Description"] as string,
                ImageUrl = new SPFieldUrlValue(it["Image"] as string).Url,
                CategoryId = categoryId
            }).ToList();

            return booksInCategory;
        }
    }
}
