using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedAccountRepository : IAccountRepository
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedAccountRepository(AccountRepository accountRepository, IMemoryCache memoryCache)
        {
            _accountRepository = accountRepository;
            _memoryCache = memoryCache;
        }
        public void CreateAccount(Account account)
        {
            string key = $"accounts";
            _accountRepository.CreateAccount(account);
            _memoryCache.Remove(key);
        }

        public void DeleteAccount(Account account)
        {
            string key = $"accounts";
            string key_1 = $"accounts-{account.Id}";
            _accountRepository.DeleteAccount(account);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }

        public async Task<Account> GetAccount(string customerId, Guid id, bool trackChanges)
        {
            string key = $"accounts-{id}";
            return await _memoryCache.GetOrCreateAsync(key, entry => 
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return _accountRepository.GetAccount(customerId, id, trackChanges);
            });
        }

        public async Task<IEnumerable<Account>> GetAccounts(string customerId, bool trackChanges)
        {
            string key = $"accounts";
            return await _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return _accountRepository.GetAccounts(customerId, trackChanges);
            });
        }

        public void UpdateAccount(Account account)
        {
            string key = $"accounts";
            string key_1 = $"accounts-{account.Id}";
            _accountRepository.UpdateAccount(account);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }
    }
}
