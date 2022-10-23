using Entities.DataTransferObjects.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Transactions;

namespace Entities.DataTransferObjects.Account
{
    public class AccountCreationDto
    {
        [Required]
        public Guid AccountStatusId { get; set; }
        public DateTime StatusUpdatedDateTime { get; set; }
        public string Currency { get; set; }
        [Required]
        public Guid AccountTypeId { get; set; }
        [Required]
        public Guid AccountSubTypeId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public DateTime OpeningDate { get; set; } = DateTime.Now;
        public decimal TotalBalance { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public ICollection<TransactionCreationDto> Transactions { get; set; }
    }
}
