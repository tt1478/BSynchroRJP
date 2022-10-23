using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Transaction
    {
        [Column("TransactionId")]
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal WithdrawAmount { get; set; }
        public decimal DepositAmount { get; set; }
        [ForeignKey("Account")]
        [Required]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
