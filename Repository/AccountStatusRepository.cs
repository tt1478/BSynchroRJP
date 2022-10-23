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
    public class AccountStatusRepository:RepositoryBase<AccountStatus>, IAccountStatusRepository
    {
        public AccountStatusRepository(RepositoryContext repository):base(repository)
        {

        }
        public void UpdateAccountStatus(AccountStatus accountStatus)
        {
            Update(accountStatus);
        }

        public void CreateAccountStatus(AccountStatus accountStatus)
        {
            Create(accountStatus);
        }

        public void DeleteAccountStatus(AccountStatus accountStatus)
        {
            Delete(accountStatus);
        }

        public async Task<AccountStatus> GetAccountStatus(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatuses(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
