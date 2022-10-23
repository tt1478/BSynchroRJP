using AutoMapper;
using BSynchroRJP.ActionFilters;
using Contracts;
using Entities.DataTransferObjects.Account;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSynchroRJP.Controllers
{
    [Route("api/customers/{customerId}/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AccountsController(IloggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccounts(string customerId)
        {
            var accounts = await _repository.Account.GetAccounts(customerId, false);
            var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return Ok(accountsDto);
        }
        [HttpGet("{id}", Name = "GetAccountById")]
        public async Task<IActionResult> GetAccount(string customerId, Guid id)
        {
            var account = await _repository.Account.GetAccount(customerId, id, false);
            if(account == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var accountDto = _mapper.Map<AccountDto>(account);
            return Ok(accountDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccount(string customerId, [FromBody]AccountCreationDto account) 
        {
            account.CustomerId = customerId;
            var accountEntity = _mapper.Map<Account>(account);
            _repository.Account.CreateAccount(accountEntity);
            await _repository.SaveAsync();
            var accountToReturn = _mapper.Map<AccountDto>(accountEntity);
            return CreatedAtRoute("GetAccountById", new { id = accountToReturn.Id }, accountToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAccountExistsAttribute))]
        public async Task<IActionResult> UpdateAccount(string customerId, Guid id, [FromBody]AccountUpdateDto account)
        {
            account.CustomerId = customerId;
            var accountEntity = HttpContext.Items["account"] as Account;
            _mapper.Map(account, accountEntity);
            _repository.Account.UpdateAccount(accountEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAccountExistsAttribute))]
        public async Task<IActionResult> DeleteAccount(string customerId, Guid id)
        {
            var account = HttpContext.Items["account"] as Account;
            _repository.Account.DeleteAccount(account);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
