using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedAccountSubTypeRepository : IAccountSubTypeRepository
    {
        private readonly AccountSubTypeRepository _accountSubTypeRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedAccountSubTypeRepository(AccountSubTypeRepository accountSubTypeRepository, IMemoryCache memoryCache)
        {
            _accountSubTypeRepository = accountSubTypeRepository;
            _memoryCache = memoryCache;
        }
        public void CreateAccountSubType(AccountSubType accountSubType)
        {
            string key = $"accountSubTypes";
            _accountSubTypeRepository.CreateAccountSubType(accountSubType);
            _memoryCache.Remove(key);
        }

        public void DeleteAccountSubType(AccountSubType accountSubType)
        {
            string key = $"accountSubTypes";
            string key_1 = $"accountSubTypes-{accountSubType.Id}";
            _accountSubTypeRepository.DeleteAccountSubType(accountSubType);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }

        public async Task<AccountSubType> GetAccountSubType(Guid id, bool trackChanges)
        {
            string key = $"accountSubTypes-{id}";
            return await _memoryCache.GetOrCreateAsync(key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountSubTypeRepository.GetAccountSubType(id, trackChanges);
                });
        }

        public async Task<IEnumerable<AccountSubType>> GetAccountSubTypes(bool trackChanges)
        {
            string key = $"accountSubTypes";
            return await _memoryCache.GetOrCreateAsync(key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountSubTypeRepository.GetAccountSubTypes(trackChanges);
                });
        }

        public void UpdateAccountSubType(AccountSubType accountSubType)
        {
            string key = $"accountSubTypes";
            string key_1 = $"accountSubTypes-{accountSubType.Id}";
            _accountSubTypeRepository.UpdateAccountSubType(accountSubType);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }
    }
}
