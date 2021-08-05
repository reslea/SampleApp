using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthSample.WebDb
{
    public class BooksRepository : IBookRepository
    {
        public static List<Book> Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Author = "Дж. Роулинг",
                Title = "и Философский камень",
                PagesCount = 300,
                PublishDate = new DateTime(1997, 3,3)
            },
        };

        private static int currentId = 2;


        public IEnumerable<Book> GetAll()
        {
            return Books;
        }

        public Book Get(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }

        public Book Create(Book book)
        {
            book.Id = currentId++;
            Books.Add(book);
            return book;
        }

        public void Update(Book book)
        {

        }

        public void Delete(Book toDelete)
        {
            Books.Remove(toDelete);
        }
    }
}
