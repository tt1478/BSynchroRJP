using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAccountStatusRepository
    {
        Task<IEnumerable<AccountStatus>> GetAccountStatuses(bool trackChanges);
        Task<AccountStatus> GetAccountStatus(Guid id, bool trackChanges);
        void CreateAccountStatus(AccountStatus accountStatus);
        void UpdateAccountStatus(AccountStatus accountStatus);
        void DeleteAccountStatus(AccountStatus accountStatus);
    }
}
