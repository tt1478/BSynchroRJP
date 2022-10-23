using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Contracts
{
    public interface IAccountTypeRepository
    {
        Task<IEnumerable<AccountType>> GetAccountTypes(bool trackChanges);
        Task<AccountType> GetAccountType(Guid id, bool trackChanges);
        void CreateAccountType(AccountType accountType);
        void UpdateAccountType(AccountType accountType);
        void DeleteAccountType(AccountType accountType);
    }
}
