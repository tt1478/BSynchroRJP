using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountSubTypeRepository: RepositoryBase<AccountSubType>, IAccountSubTypeRepository
    {
        public AccountSubTypeRepository(RepositoryContext repository):base(repository)
        {

        }
        public void UpdateAccountSubType(AccountSubType accountSubType)
        {
            Update(accountSubType);
        }

        public void CreateAccountSubType(AccountSubType accountSubType)
        {
            Create(accountSubType);
        }

        public void DeleteAccountSubType(AccountSubType accountSubType)
        {
            Delete(accountSubType);
        }

        public async Task<AccountSubType> GetAccountSubType(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AccountSubType>> GetAccountSubTypes(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
