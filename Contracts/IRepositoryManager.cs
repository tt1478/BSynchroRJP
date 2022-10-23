using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        public IAccountRepository Account { get; }
        public IAccountStatusRepository AccountStatus { get; }
        public IAccountSubTypeRepository AccountSubType { get; }
        public IAccountTypeRepository AccountType { get; }
        public ITransactionRepository Transaction { get; }
        Task SaveAsync();
    }
}
