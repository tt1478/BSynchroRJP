using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.AccountStatus
{
    public class AccountStatusUpdateDto
    {
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Name { get; set; }
        public ICollection<AccountCreationDto> Accounts { get; set; }
    }
}
