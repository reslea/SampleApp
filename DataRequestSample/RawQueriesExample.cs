using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRequestSample
{
    public static class RawQueriesExample
    {
        public static async Task SelectViaSchema(SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM Books", connection);
            using var reader = await command.ExecuteReaderAsync();

            var schema = reader.GetColumnSchema();

            var columnNames = schema.Select(c => c.ColumnName).ToList();
            while (await reader.ReadAsync())
            {
                foreach (var columnName in columnNames)
                {
                    Console.WriteLine($"{columnName}: {reader[columnName]}");
                }
            }
        }

        public static async Task InsertBookAsync(Book book, SqlConnection connection)
        {
            await using var transaction = await connection.BeginTransactionAsync();

            var insertCommand = "INSERT INTO Books (Title, Author, PagesCount, PublishDate) VALUES " +
                                "(@title, @author, @pagesCount, @PublishDate)";
            var command = new SqlCommand(insertCommand, connection);
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@pagesCount", book.PagesCount);
            command.Parameters.AddWithValue("@PublishDate", book.PublishDate);

            await command.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
        }

        public static async Task GetBooksQueryAsync(SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM Books", connection);

            using var reader = await command.ExecuteReaderAsync();

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
                    Id = (int)reader[nameof(Book.Id)],
                    Title = (string)reader[nameof(Book.Title)],
                    Author = (string)reader[nameof(Book.Author)],
                    PagesCount = (int)reader[nameof(Book.PagesCount)],
                    PublishDate = (DateTime)reader[nameof(Book.PublishDate)]
                };
            }
        }
    }
}
