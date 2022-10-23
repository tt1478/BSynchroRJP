using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasData(
                new AccountType
                {
                    Id = Guid.NewGuid(),
                    Type = "Business"
                },
                new AccountType
                {
                    Id = Guid.NewGuid(),
                    Type = "Personal"
                });
        }
    }
}
