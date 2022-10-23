using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.AccountType
{
    public class AccountTypeDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }
    }
}
