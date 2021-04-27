using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Library.Data;
using Library.Data.Entity;
using Library.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Library.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var context = new LibraryContext();
            //var bookRepository = RepositoryFactory.CreateBookRepository(context);
            //var bookPriceRepo = RepositoryFactory.CreateBookPriceRepository(context);

            var serviceLocator = new ServiceLocator();
            var bookRepository = serviceLocator.Get<IBookRepository>();
            var bookPriceRepository = serviceLocator.Get<IBookPriceRepository>();

            var book = new Book
            {
                Title = "Гарри Поттер и Философский камень",
                Author = "Дж. Роулинг",
                PublishDate = new DateTime(2007, 4, 19),
                PagesCount = 400,
                Genre = "Фэнтези"
            };
            bookRepository.Create(book);
            bookPriceRepository.Create(new BookPrice
            {
                Book = book,
                Price = 400
            });

            //context.SaveChanges();

            var rowlingBooks = bookRepository.Get("Гарри Поттер");

            var rowlingBooksList = rowlingBooks.ToList();
        }
    }
}
