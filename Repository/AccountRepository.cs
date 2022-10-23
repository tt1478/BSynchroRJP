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
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repository):base(repository)
        {

        }
        public void UpdateAccount(Account account)
        {
            Update(account);
        }
        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

        public async Task<Account> GetAccount(string customerId, Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id) && a.CustomerId.Equals(customerId), trackChanges)
                        .Include(a => a.AccountStatus)
                        .Include(a => a.AccountSubType)
                        .Include(a => a.AccountType)
                        .Include(a => a.Customer)
                        .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAccounts(string customerId, bool trackChanges)
        {
            return await FindByCondition(a => a.CustomerId.Equals(customerId), trackChanges)
                        .Include(a => a.AccountStatus)
                        .Include(a => a.AccountSubType)
                        .Include(a => a.AccountType)
                        .Include(a => a.Customer)
                        .ToListAsync();
        }
    }
}
