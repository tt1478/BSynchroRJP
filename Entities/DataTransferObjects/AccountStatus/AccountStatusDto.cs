using Entities.DataTransferObjects.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.AccountStatus
{
    public class AccountStatusDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }
    }
}
