using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class AccountStatusConfiguration : IEntityTypeConfiguration<AccountStatus>
    {
        public void Configure(EntityTypeBuilder<AccountStatus> builder)
        {
            builder.HasData(
                 new AccountStatus
                 {
                     Id = Guid.NewGuid(),
                     Name = "Enabled"
                 },
                 new AccountStatus
                 {
                     Id = Guid.NewGuid(),
                     Name = "Disabled"
                 },
                 new AccountStatus
                 {
                     Id = Guid.NewGuid(),
                     Name = "Deleted"
                 },
                 new AccountStatus
                 {
                     Id = Guid.NewGuid(),
                     Name = "ProForma"
                 },
                 new AccountStatus
                 {
                     Id = Guid.NewGuid(),
                     Name = "Pending"
                 });
        }
    }
}
