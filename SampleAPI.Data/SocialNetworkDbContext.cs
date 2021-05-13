using System;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Data.Entities;

namespace SampleAPI.Data
{
    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SocialNetworkDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
