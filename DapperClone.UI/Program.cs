using System;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DapperClone.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
            using var connection = new SqlConnection(connectionString);

            //var book = new Book
            //{
            //    Author = "Дж. Роулинг",
            //    Title = "Гарри Поттер и Орден Феникса",
            //    PagesCount = 700,
            //    PublishDate = new DateTime(2006, 7, 18)
            //};
            //connection.Execute("INSERT INTO Books " +
            //                   "(Title, Author, PagesCount, PublishDate) VALUES " +
            //                   "(@Title, @Author, @PagesCount, @PublishDate)", book);

            //var booksQuery = "SELECT * FROM Books WHERE Id = @Id";
            //var book = connection.QueryFirstOrDefault<Book>(booksQuery, new Book { Id = 1 });

            //Console.WriteLine($"{book.PublishDate}: {book.Title} {book.Author} {book.PagesCount}");

            //var booksQuery = "SELECT * FROM Books";
            //var books = connection.Query<Book>(booksQuery);

            //foreach (var book in books)
            //{
            //    Console.WriteLine($"{book.PublishDate}: {book.Title} {book.Author} {book.PagesCount}");
            //}

            var booksQuery = "SELECT * FROM Books";
            var books = connection.QueryAsync<Book>(booksQuery);

            await foreach (var book in books)
            {
                Console.WriteLine($"{book.PublishDate}: {book.Title} {book.Author} {book.PagesCount}");
            }
        }
    }
}
