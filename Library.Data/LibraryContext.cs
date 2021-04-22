using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Library.Data.Entity;
using Library.Data.EntityConfiguration;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new BookConfiguration());
            //modelBuilder.ApplyConfiguration(new BookPriceConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }

}
