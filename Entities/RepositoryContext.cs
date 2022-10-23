using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext: IdentityDbContext<Customer>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AccountStatusConfiguration());
            builder.ApplyConfiguration(new AccountSubTypeConfiguration());
            builder.ApplyConfiguration(new AccountTypeConfiguration());
        }
        public DbSet<AccountSubType> accountSubTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
