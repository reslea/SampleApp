using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataRequestSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Books", connection);

            using var reader = command.ExecuteReader();

            var books = GetBooks(reader);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id} {book.Author} {book.PagesCount}");
            }
        }

        public static IEnumerable<Book> GetBooks(SqlDataReader reader)
        {
            while (reader.Read())
            {
                yield return new Book
                {
                    Id = (int)reader[0],
                    Title = (string)reader[1],
                    Author = (string)reader[2],
                    PagesCount = (int)reader[3],
                    PublishDate = (DateTime)reader[4]
                };
            }
        }
    }
}
