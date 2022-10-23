using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.AccountSubType
{
    public class AccountSubTypeUpdateDto
    {
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string SubType { get; set; }
        public ICollection<AccountCreationDto> Accounts { get; set; }
    }
}
