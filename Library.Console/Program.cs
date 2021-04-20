﻿using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new LibraryContext();
            context.Database.EnsureCreated();

            var book = new Book
            {
                Title = "Гарри Поттер и чтото там",
                Author = "Дж. Роулинг",
                PublishDate = new DateTime(2007, 4, 19),
                PagesCount = 400
            };
            context.Books.Add(book);
            context.BookPriceses.Add(new BookPrice
            {
                Book = book,
                Price = 400
            });
            context.SaveChanges();
        }
    }
}