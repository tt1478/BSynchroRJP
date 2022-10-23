using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class AccountType
    {
        [Column("AccountTypeId")]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Type { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
