using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataRequestSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            #region SELECT
            // SELECT
            //await RawQueriesExample.GetBooksQueryAsync(connection);
            #endregion

            #region INSERT
            // INSERT
            //var book = new Book
            //{
            //    Title = "Гарри Поттер и Кубок Огня",
            //    Author = "Дж. Роулинг",
            //    PagesCount = 600,
            //    PublishDate = new DateTime(2004, 10, 11)
            //};

            //await RawQueriesExample.InsertBookAsync(book, connection);
            #endregion

            #region SCHEMA
            //await RawQueriesExample.SelectViaSchema(connection);
            #endregion

            using var bookQuery = new Repository<Book>(connection);

            var books = await bookQuery.GetAll();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id} {book.Title} {book.Author} {book.PagesCount} {book.PublishDate}");
            }

            IRepository<Book> bookRepo;
        }


    }
}
