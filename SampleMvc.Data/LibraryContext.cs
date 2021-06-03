using Microsoft.EntityFrameworkCore;
using SampleMvc.Data.Entity;

namespace SampleMvc.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookPrice> BookPrices { get; set; }

        public DbSet<User> Users { get; set; }

        public LibraryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new BookConfiguration());
            //modelBuilder.ApplyConfiguration(new BookPriceConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }

}
