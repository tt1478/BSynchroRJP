using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions(Guid accountId, bool trackChanges);
        Task<Transaction> GetTransaction(Guid accountId, Guid id, bool trackChanges);
        void CreateTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}
