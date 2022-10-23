using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedAccountStatusRepository : IAccountStatusRepository
    {
        private readonly AccountStatusRepository _accountStatusRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedAccountStatusRepository(AccountStatusRepository accountStatusRepository, IMemoryCache memoryCache)
        {
            _accountStatusRepository = accountStatusRepository;
            _memoryCache = memoryCache;
        }
        public void CreateAccountStatus(AccountStatus accountStatus)
        {
            string key = $"accountStatuses";
            _accountStatusRepository.CreateAccountStatus(accountStatus);
            _memoryCache.Remove(key);
        }

        public void DeleteAccountStatus(AccountStatus accountStatus)
        {
            string key = $"accountStatuses";
            string key_1 = $"accountStatuses-{accountStatus.Id}";
            _accountStatusRepository.DeleteAccountStatus(accountStatus);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }

        public async Task<AccountStatus> GetAccountStatus(Guid id, bool trackChanges)
        {
            string key = $"accountStatuses-{id}";
            return await _memoryCache.GetOrCreateAsync(key,
                entry => 
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountStatusRepository.GetAccountStatus(id, trackChanges);
                });
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatuses(bool trackChanges)
        {
            string key = $"accountStatuses";
            return await _memoryCache.GetOrCreateAsync(key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                    return _accountStatusRepository.GetAccountStatuses(trackChanges);
                });
        }

        public void UpdateAccountStatus(AccountStatus accountStatus)
        {
            string key = $"accountStatuses";
            string key_1 = $"accountStatuses-{accountStatus.Id}";
            _accountStatusRepository.UpdateAccountStatus(accountStatus);
            _memoryCache.Remove(key);
            _memoryCache.Remove(key_1);
        }
    }
}
