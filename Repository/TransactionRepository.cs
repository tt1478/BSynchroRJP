using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TransactionRepository:RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(RepositoryContext repository):base(repository)
        {

        }
        public void UpdateTransaction(Transaction transaction)
        {
            Update(transaction);
        }

        public void CreateTransaction(Transaction transaction)
        {
            Create(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            Delete(transaction);
        }

        public async Task<Transaction> GetTransaction(Guid accountId, Guid id, bool trackChanges)
        {
            return await FindByCondition(t => t.Id.Equals(id) && t.AccountId.Equals(accountId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(Guid accountId,bool trackChanges)
        {
            return await FindByCondition(t => t.AccountId.Equals(accountId), trackChanges).ToListAsync();
        }
    }
}
