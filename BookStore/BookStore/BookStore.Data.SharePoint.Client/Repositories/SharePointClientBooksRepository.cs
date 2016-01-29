using BookStore.Data.Model;
using BookStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.SharePoint.Client.Repositories
{
    public class SharePointClientBooksRepository : IBooksRepository
    {
        private const string ListName = "Books";

        public List<Book> GetByCategory(int categoryId)
        {
            //TODO: Implement this method using CSOM
            return new List<Book>();
        }
    }
}
