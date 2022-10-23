using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedTransactionRepository : ITransactionRepository
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedTransactionRepository(TransactionRepository transactionRepository, IMemoryCache memoryCache)
        {
            _transactionRepository = transactionRepository;
            _memoryCache = memoryCache;
        }
        public void CreateTransaction(Transaction transaction)
        {
            string key = $"transactions";
            _transactionRepository.CreateTransaction(transaction);
            _memoryCache.Remove(key);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            string key = $"transactions";
            string key_1 = $"transactions-{transaction.Id}";
            _transactionRepository.DeleteTransaction(transaction);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }

        public async Task<Transaction> GetTransaction(Guid accountId, Guid id, bool trackChanges)
        {
            string key = $"transactions-{id}";
            return await _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return _transactionRepository.GetTransaction(accountId, id, trackChanges);
            });
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(Guid accountId, bool trackChanges)
        {
            string key = $"transactions";
            return await _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return _transactionRepository.GetTransactions(accountId, trackChanges);
            });
        }

        public void UpdateTransaction(Transaction transaction)
        {
            string key = $"transactions";
            string key_1 = $"transactions-{transaction.Id}";
            _transactionRepository.UpdateTransaction(transaction);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }
    }
}
