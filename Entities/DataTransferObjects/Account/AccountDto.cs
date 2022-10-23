using Entities.DataTransferObjects.AccountStatus;
using Entities.DataTransferObjects.AccountSubType;
using Entities.DataTransferObjects.AccountType;
using Entities.DataTransferObjects.Transaction;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.Account
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public Guid AccountStatusId { get; set; }
        public AccountStatusDto AccountStatus { get; set; }
        public DateTime StatusUpdatedDateTime { get; set; }
        public string Currency { get; set; }
        public Guid AccountTypeId { get; set; }
        public AccountTypeDto AccountType { get; set; }
        public Guid AccountSubTypeId { get; set; }
        public AccountSubTypeDto AccountSubType { get; set; }
        public string CustomerId { get; set; }
        public DateTime OpeningDate { get; set; } = DateTime.Now;
        public decimal TotalBalance { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public ICollection<TransactionDto> Transactions { get; set; }
    }
}
