using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.Transaction
{
    public class TransactionUpdateDto
    {
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal WithdrawAmount { get; set; }
        public decimal DepositAmount { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
}
