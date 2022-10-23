using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class AccountSubType
    {
        [Column("AccountSubTypeId")]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string SubType { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
