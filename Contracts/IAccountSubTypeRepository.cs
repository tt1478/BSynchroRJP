using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IAccountSubTypeRepository
    {
        Task<IEnumerable<AccountSubType>> GetAccountSubTypes(bool trackChanges);
        Task<AccountSubType> GetAccountSubType(Guid id, bool trackChanges);
        void CreateAccountSubType(AccountSubType accountSubType);
        void UpdateAccountSubType(AccountSubType accountSubType);
        void DeleteAccountSubType(AccountSubType accountSubType);
    }
}
