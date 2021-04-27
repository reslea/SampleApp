using System.Collections.Generic;
using Library.Data.Entity;

namespace Library.Data.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> Get(string title);
    }
}
