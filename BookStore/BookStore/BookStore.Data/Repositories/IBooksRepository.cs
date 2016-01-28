using BookStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public interface IBooksRepository
    {
        List<Book> GetByCategory(int categoryId);
    }
}
