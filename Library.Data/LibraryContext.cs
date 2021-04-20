using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookPrice> BookPriceses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EfLibrary;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class BookPrice
    {
        public int Id { get; set; }
        
        [Required]
        public Book Book { get; set; }

        //public int BookId { get; set; }

        public decimal Price { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }
        
        [Required, StringLength(255)]
        public string Author { get; set; }

        public int PagesCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
