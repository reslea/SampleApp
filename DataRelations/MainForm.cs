using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRelations
{
    public partial class MainForm : Form, IDisposable
    {
        private SqlDataAdapter booksDataAdapter;
        private DataSet dataset;
        private DataTable booksTable;
        private DataTable bookPricesTable;
        private SqlConnection _connection;

        public MainForm()
        {
            InitializeComponent();
            InitializeDataSet();
            //FillBooksTable();
            //FillBookPricesTable();

            InitializeDataAdapter();

            BooksDataGrid.DataSource = dataset.Tables["Books"];
            BookPricesDataGrid.DataSource = dataset.Tables["BookPrices"];
        }

        private void InitializeDataAdapter()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            
            booksDataAdapter = new SqlDataAdapter("SELECT * FROM Books; SELECT * FROM BookPrices;", _connection);
            booksDataAdapter.TableMappings.Add("Table", "Books");
            booksDataAdapter.TableMappings.Add("Table1", "BookPrices");
            
            var commandBuilder = new SqlCommandBuilder(booksDataAdapter);

            booksDataAdapter.Fill(dataset);
        }

        private void FillBookPricesTable()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM BookPrices", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FillBookPrices(reader, dataset.Tables["BookPrices"]);
                    }
                }
            }
        }
        
        private void FillBookPrices(SqlDataReader reader, DataTable dataTable)
        {
            var row = dataTable.NewRow();
            var id = (int)reader[nameof(Book.Id)];

            row[nameof(BookPrice.Id)] = id;
            row[nameof(BookPrice.BookId)] = reader[nameof(BookPrice.BookId)];
            row[nameof(BookPrice.Price)] = reader[nameof(BookPrice.Price)];

            dataTable.Rows.Add(row);
            row.AcceptChanges();
        }

        public void InitializeDataSet()
        {
            dataset = new DataSet();

            var books = new DataTable("Books");
            dataset.Tables.Add(books);
            //books.Columns.Add(new DataColumn(nameof(Book.Id), typeof(int)) { AllowDBNull = false });
            //books.Columns.Add(new DataColumn(nameof(Book.Title), typeof(string)) { AllowDBNull = false });
            //books.Columns.Add(new DataColumn(nameof(Book.Author), typeof(string)) { AllowDBNull = false });
            //books.Columns.Add(new DataColumn(nameof(Book.PagesCount), typeof(int)) { AllowDBNull = false });
            //books.Columns.Add(new DataColumn(nameof(Book.PublishDate), typeof(DateTime)) { AllowDBNull = false });

            var bookPrices = new DataTable("BookPrices");
            dataset.Tables.Add(bookPrices);
            //bookPrices.Columns.Add(new DataColumn(nameof(BookPrice.Id), typeof(int)) { AllowDBNull = false });
            //bookPrices.Columns.Add(new DataColumn(nameof(BookPrice.BookId), typeof(int)) { AllowDBNull = false });
            //bookPrices.Columns.Add(new DataColumn(nameof(BookPrice.Price), typeof(decimal)) { AllowDBNull = false });

            dataset.Relations.Add(books.Columns["Id"], bookPrices.Columns["BookId"]);

            booksTable = books;
            bookPricesTable = bookPrices;
        }

        private void FillBooksTable()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Books", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FillBooks(reader, dataset.Tables["Books"]);
                    }
                }
            }
        }

        private static void FillBooks(SqlDataReader reader, DataTable dataTable)
        {
            var row = dataTable.NewRow();
            var id = (int)reader[nameof(Book.Id)];

            row[nameof(Book.Id)] = id;
            row[nameof(Book.Author)] = reader[nameof(Book.Author)];
            row[nameof(Book.Title)] = reader[nameof(Book.Title)];
            row[nameof(Book.PagesCount)] = reader[nameof(Book.PagesCount)];
            row[nameof(Book.PublishDate)] = reader[nameof(Book.PublishDate)];

            dataTable.Rows.Add(row);
            row.AcceptChanges();
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {

        }

        private async void FreezeButton_Click(object sender, EventArgs e)
        {            
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsCompletedLabel.Text = $"Completed on {Thread.CurrentThread.ManagedThreadId}";
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            booksDataAdapter.Update(dataset);
        }

        public new void Dispose()
        {
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
