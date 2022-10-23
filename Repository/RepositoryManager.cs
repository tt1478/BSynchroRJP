using Contracts;
using Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager:IRepositoryManager
    {
        private IAccountRepository _accountRepository;
        private IAccountStatusRepository _accountStatusRepository;
        private IAccountSubTypeRepository _accountSubTypeRepository;
        private IAccountTypeRepository _accountTypeRepository;
        private ITransactionRepository _transactionRepository;
        private RepositoryContext _repository;
        private IMemoryCache _memoryCache;
        
        public RepositoryManager(RepositoryContext repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public IAccountRepository Account 
        {
            get
            {
                if(_accountRepository == null)
                {
                    _accountRepository = new CachedAccountRepository(new AccountRepository(_repository), _memoryCache);
                }
                return _accountRepository;
            }
        }

        public IAccountStatusRepository AccountStatus 
        {
            get
            {
                if(_accountStatusRepository == null)
                {
                    _accountStatusRepository = new CachedAccountStatusRepository(new AccountStatusRepository(_repository), _memoryCache);
                }
                return _accountStatusRepository;
             }
        }

        public IAccountSubTypeRepository AccountSubType 
        {
            get 
            {
                if(_accountSubTypeRepository == null)
                {
                    _accountSubTypeRepository = new CachedAccountSubTypeRepository(new AccountSubTypeRepository(_repository), _memoryCache);
                }
                return _accountSubTypeRepository;
            }
        }

        public IAccountTypeRepository AccountType 
        {
            get 
            {
                if(_accountTypeRepository == null)
                {
                    _accountTypeRepository = new CachedAccountTypeRepository(new AccountTypeRepository(_repository), _memoryCache);
                }
                return _accountTypeRepository;
            }
        }

        public ITransactionRepository Transaction
        {
            get 
            {
                if(_transactionRepository == null)
                {
                    _transactionRepository = new CachedTransactionRepository(new TransactionRepository(_repository), _memoryCache);
                }
                return _transactionRepository;
            }
        }
        public async Task SaveAsync()
        {
           await _repository.SaveChangesAsync();
        }
    }
}
