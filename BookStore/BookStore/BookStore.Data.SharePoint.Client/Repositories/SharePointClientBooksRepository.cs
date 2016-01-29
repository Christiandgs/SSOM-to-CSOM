using BookStore.Data.Model;
using BookStore.Data.Repositories;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.Data.SharePoint.Client.Repositories
{
    public class SharePointClientBooksRepository : IBooksRepository
    {
        private const string ListName = "Books";

        public List<Book> GetByCategory(int categoryId)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext.Current);
            var clientContext = spContext.CreateUserClientContextForSPHost();

            var web = clientContext.Web;

            var booksList = web.Lists.GetByTitle(ListName);

            var query = new CamlQuery();
            query.ViewXml = string.Format(
@"<View>
    <Query>
        <Where>
            <Eq>
                <FieldRef Name='Category' LookupId='TRUE' />
                <Value Type='Lookup'>{0}</Value>
            </Eq>
        </Where>
    </Query>
</View>", categoryId);

            var items = booksList.GetItems(query);

            clientContext.Load(items);
            clientContext.ExecuteQueryRetry();

            var booksInCategory = items.OfType<ListItem>().Select(it => new Book
            {
                Id = it.Id,
                Title = it["Title"] as string,
                Description = it["Description"] as string,
                ImageUrl = ((FieldUrlValue)it["Image"]).Url,
                CategoryId = categoryId
            }).ToList();

            return booksInCategory;
        }
    }
}
