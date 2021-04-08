using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Disconnected
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(nameof(Book.Id), typeof(int));
            dataTable.Columns.Add(nameof(Book.Title), typeof(string));
            dataTable.Columns.Add(nameof(Book.Author), typeof(string));
            dataTable.Columns.Add(nameof(Book.PagesCount), typeof(int));
            dataTable.Columns.Add(nameof(Book.PublishDate), typeof(DateTime));

            //FillDataTableManually(dataTable);

            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Books", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                FillDataTableAutomatically(reader, dataTable);
            }
            connection.Close();

            Console.WriteLine(dataTable.Rows.Count);
        }

        private static void FillDataTableAutomatically(SqlDataReader reader, DataTable dataTable)
        {
            var row = dataTable.NewRow();
            row[nameof(Book.Id)] = reader[nameof(Book.Id)];
            row[nameof(Book.Author)] = reader[nameof(Book.Author)];
            row[nameof(Book.Title)] = reader[nameof(Book.Title)];
            row[nameof(Book.PagesCount)] = reader[nameof(Book.PagesCount)];
            row[nameof(Book.PublishDate)] = reader[nameof(Book.PublishDate)];

            dataTable.Rows.Add(row);
        }

        private static void FillDataTableManually(DataTable dataTable)
        {
            var row = dataTable.NewRow();
            row[nameof(Book.Id)] = "1";
            row[nameof(Book.Title)] = "1984";
            row[nameof(Book.Author)] = "Дж. Оруэлл";
            row[nameof(Book.PagesCount)] = 800;
            row[nameof(Book.PublishDate)] = new DateTime(2008, 1, 1);
            dataTable.Rows.Add(row);
        }
    }
}
