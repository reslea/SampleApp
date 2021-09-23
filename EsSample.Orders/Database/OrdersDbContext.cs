using EsSample.Orders.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsSample.Orders.Database
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderCheckpoint> OrderCheckpoints { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
        }
    }
}
