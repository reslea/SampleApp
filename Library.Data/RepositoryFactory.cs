using System;
using System.Collections.Generic;
using System.Text;
using Library.Data.Repository;

namespace Library.Data
{
    public static class RepositoryFactory
    {
        public static IBookRepository CreateBookRepository(LibraryContext context)
        {
            return new BookRepository(context);
        }

        public static IBookPriceRepository CreateBookPriceRepository(LibraryContext context)
        {
            return new BookPriceRepository(context);
        }
    }
}
