using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Transaction
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal WithdrawAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public Guid AccountId { get; set; }
        public AccountDto Account { get; set; }
    }
}
