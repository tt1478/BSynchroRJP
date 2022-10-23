using AutoMapper;
using Entities.DataTransferObjects.Account;
using Entities.DataTransferObjects.AccountStatus;
using Entities.DataTransferObjects.AccountSubType;
using Entities.DataTransferObjects.AccountType;
using Entities.DataTransferObjects.Customer;
using Entities.DataTransferObjects.Transaction;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSynchroRJP
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Account
            CreateMap<Account, AccountDto>();
            CreateMap<AccountCreationDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
            #endregion
            #region AccountStatus
            CreateMap<AccountStatus, AccountStatusDto>();
            CreateMap<AccountStatusCreationDto, AccountStatus>();
            CreateMap<AccountStatusUpdateDto, AccountStatus>();
            #endregion
            #region AccountSubType
            CreateMap<AccountSubType, AccountSubTypeDto>();
            CreateMap<AccountSubTypeCreationDto, AccountSubType>();
            CreateMap<AccountSubTypeUpdateDto, AccountSubType>();
            #endregion
            #region AccountType
            CreateMap<AccountType, AccountTypeDto>();
            CreateMap<AccountTypeCreationDto, AccountType>();
            CreateMap<AccountTypeUpdateDto, AccountType>();
            #endregion
            #region Customer
            CreateMap<CustomerForRegistrationDto, Customer>();
            #endregion
            #region Transaction
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionCreationDto, Transaction>();
            CreateMap<TransactionUpdateDto, Transaction>();
            #endregion
        }
    }
}
