using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Account
    {
        [Column("AccountId")]
        public Guid Id { get; set; }
        [ForeignKey("AccountStatus")]
        [Required]
        public Guid AccountStatusId { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public DateTime StatusUpdatedDateTime { get; set; }
        public string Currency { get; set; }
        [ForeignKey("AccountType")]
        [Required]
        public Guid AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        [ForeignKey("AccountSubType")]
        [Required]
        public Guid AccountSubTypeId { get; set; }
        public AccountSubType AccountSubType { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public string CustomerId { get; set; }
        public Customer Customer  { get; set; }
        public DateTime OpeningDate { get; set; } = DateTime.Now;
        public decimal TotalBalance { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Transaction> Transactions { get; set; }
    }
}

