using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Disconnected
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataset =new DataSet();

            var dataTable = new DataTable();
            dataset.Tables.Add(dataTable);

            dataTable.Columns.Add(new DataColumn(nameof(Book.Id), typeof(int)) { AllowDBNull = false });
            dataTable.Columns.Add(new DataColumn(nameof(Book.Title), typeof(string)) { AllowDBNull = false });
            dataTable.Columns.Add(new DataColumn(nameof(Book.Author), typeof(string)) { AllowDBNull = false });
            dataTable.Columns.Add(new DataColumn(nameof(Book.PagesCount), typeof(int)) { AllowDBNull = false });
            dataTable.Columns.Add(new DataColumn(nameof(Book.PublishDate), typeof(DateTime)) { AllowDBNull = false });
            
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

            InsertRow(dataTable, new Book
            {
                Title = "Гарри Поттер и Дары Смерти",
                Author = "Дж. Роулинг",
                PublishDate = new DateTime(2011, 1, 1),
                PagesCount = 800
            });

            var rows = dataTable.Rows.Cast<DataRow>();

            connection.Open();

            foreach (DataRow row in dataTable.Rows)
            {
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        ApplyInsert(connection, row);
                        break;
                    case DataRowState.Modified:
                        break;
                    case DataRowState.Deleted:
                        break;
                }
            }

            Console.WriteLine(dataTable.Rows.Count);
        }

        private static void ApplyInsert(SqlConnection connection, DataRow row)
        {
            var command = new SqlCommand(
                "INSERT INTO Books (Author, Title, PagesCount, PublishDate) " +
                "VALUES (@Author, @Title, @PagesCount, @PublishDate)", connection);
            command.Parameters.AddWithValue(nameof(Book.Author), row[nameof(Book.Author)]);
            command.Parameters.AddWithValue(nameof(Book.Title), row[nameof(Book.Title)]);
            command.Parameters.AddWithValue(nameof(Book.PagesCount), row[nameof(Book.PagesCount)]);
            command.Parameters.AddWithValue(nameof(Book.PublishDate), row[nameof(Book.PublishDate)]);
            command.ExecuteNonQuery();
        }

        private static void InsertRow(DataTable dataTable, Book book)
        {
            var row = dataTable.NewRow();
            row[nameof(Book.Id)] = 0;
            row[nameof(Book.Author)] = book.Author;
            row[nameof(Book.Title)] = book.Title;
            row[nameof(Book.PagesCount)] = book.PagesCount;
            row[nameof(Book.PublishDate)] = book.PublishDate;

            dataTable.Rows.Add(row);
        }
        
        private static void FillDataTableAutomatically(SqlDataReader reader, DataTable dataTable)
        {
            var row = dataTable.NewRow();
            var id = (int)reader[nameof(Book.Id)];

            row[nameof(Book.Id)] = id;
            row[nameof(Book.Author)] = reader[nameof(Book.Author)];
            row[nameof(Book.Title)] = reader[nameof(Book.Title)];
            row[nameof(Book.PagesCount)] = reader[nameof(Book.PagesCount)];
            row[nameof(Book.PublishDate)] = reader[nameof(Book.PublishDate)];
            row.AcceptChanges();

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
