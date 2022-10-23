using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountTypeRepository:RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(RepositoryContext repository):base(repository)
        {

        }
        public void UpdateAccountType(AccountType accountType)
        {
            Update(accountType);
        }

        public void CreateAccountType(AccountType accountType)
        {
            Create(accountType);
        }

        public void DeleteAccountType(AccountType accountType)
        {
            Delete(accountType);
        }

        public async Task<AccountType> GetAccountType(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
