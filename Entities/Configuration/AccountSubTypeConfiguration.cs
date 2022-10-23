using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class AccountSubTypeConfiguration : IEntityTypeConfiguration<AccountSubType>
    {
        public void Configure(EntityTypeBuilder<AccountSubType> builder)
        {
            builder.HasData(
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "ChargeCard"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "CreditCard"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "CurrentAccount"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "EMoney Loan"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "Mortgage"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "PrePaidCard"
                },
                new AccountSubType
                {
                    Id = Guid.NewGuid(),
                    SubType = "Savings"
                });
        }
    }
}
