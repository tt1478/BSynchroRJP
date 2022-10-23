using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedAccountTypeRepository : IAccountTypeRepository
    {
        private readonly AccountTypeRepository _accountTypeRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedAccountTypeRepository(AccountTypeRepository accountTypeRepository, IMemoryCache memoryCache)
        {
            _accountTypeRepository = accountTypeRepository;
            _memoryCache = memoryCache;
        }
        public void CreateAccountType(AccountType accountType)
        {
            string key = $"accountTypes";
            _accountTypeRepository.CreateAccountType(accountType);
            _memoryCache.Remove(key);
        }

        public void DeleteAccountType(AccountType accountType)
        {
            string key = $"accountTypes";
            string key_1 = $"accountTypes-{accountType.Id}";
            _accountTypeRepository.DeleteAccountType(accountType);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }

        public async Task<AccountType> GetAccountType(Guid id, bool trackChanges)
        {
            string key = $"accountTypes-{id}";
            return await _memoryCache.GetOrCreateAsync(key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountTypeRepository.GetAccountType(id, trackChanges);
                });
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes(bool trackChanges)
        {
            string key = $"accountTypes";
            return await _memoryCache.GetOrCreateAsync(key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountTypeRepository.GetAccountTypes(trackChanges);
                });
        }

        public void UpdateAccountType(AccountType accountType)
        {
            string key = $"accountTypes";
            string key_1 = $"accountTypes-{accountType.Id}";
            _accountTypeRepository.UpdateAccountType(accountType);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }
    }
}
