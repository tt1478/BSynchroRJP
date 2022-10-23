using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.AccountSubType
{
    public class AccountSubTypeDto
    {
        public Guid Id { get; set; }
        public string SubType { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }
    }
}
