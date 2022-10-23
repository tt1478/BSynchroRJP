using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccounts(string customerId, bool trackChanges);
        Task<Account> GetAccount(string customerId, Guid id, bool trackChanges);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
    }
}
