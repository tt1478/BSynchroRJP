using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.AccountType
{
    public class AccountTypeUpdateDto
    {
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Type { get; set; }
        public ICollection<AccountCreationDto> Accounts { get; set; }
    }
}
