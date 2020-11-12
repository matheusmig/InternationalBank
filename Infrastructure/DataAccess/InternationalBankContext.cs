using Domain.Accounts;
using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.DataAccess
{
    public sealed class InternationalBankContext : DbContext
    {
        public InternationalBankContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts {get; set;}
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Debit> Debits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InternationalBankContext).Assembly);
            SeedData.Seed(modelBuilder);
        }
    }
}
