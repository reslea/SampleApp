using System.Collections.Generic;
using System.Linq;
using Library.Data.Entity;

namespace Library.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public IEnumerable<Book> Get(string title)
        {
            return DbSet
                .Where(b => b.Title.Contains(title))
                .ToList();
        }

        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}
