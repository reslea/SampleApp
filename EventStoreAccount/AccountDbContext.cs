using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EventStoreAccount
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountState> AccountStates { get; set; }

        public DbSet<AccountStateCheckpoint> AccountStateCheckpoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\accountDb.db");
        }
    }
}
