using Library.Data.Entity;

namespace Library.Data.Repository
{
    public class BookPriceRepository : Repository<BookPrice>, IBookPriceRepository
    {
        public BookPriceRepository(LibraryContext context) : base(context)
        {
        }
    }
}
