using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class AccountStatus
    {
        [Column("AccountStatusId")]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
