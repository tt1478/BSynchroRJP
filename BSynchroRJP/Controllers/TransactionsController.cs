using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Transaction;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using BSynchroRJP.ActionFilters;

namespace BSynchroRJP.Controllers
{
    [Route("api/customers/{customerId}/accounts/{accountId}/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public TransactionsController(IloggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactions(string customerId, Guid accountId)
        {
            var transactions = await _repository.Transaction.GetTransactions(accountId, false);
            var transactionsDto = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionsDto);
        }
        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransaction(string customerId, Guid accountId, Guid id)
        {
            var transaction = await _repository.Transaction.GetTransaction(accountId, id, false);
            if(transaction == null)
            {
                _logger.LogInfo($"Transaction with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTransaction(string customerId, Guid accountId, [FromBody]TransactionCreationDto transaction)
        {
            var account = await _repository.Account.GetAccount(customerId, accountId, false);
            if(account == null)
            {
                _logger.LogInfo($"Account with id: {accountId} doesn't exist in the database."); 
                return NotFound();
            }
            transaction.AccountId = accountId;
            var transactionEntity = _mapper.Map<Transaction>(transaction);
            _repository.Transaction.CreateTransaction(transactionEntity);
            await _repository.SaveAsync();
            var transactionToReturn = _mapper.Map<TransactionDto>(transactionEntity);
            return CreatedAtRoute("GetTransactionById", new { id = transactionToReturn.Id }, transactionToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateTransactionForAccountExistsAttribute))]
        public async Task<IActionResult> UpdateTransaction(string customerId, Guid accountId, Guid id, [FromBody]TransactionUpdateDto transaction)
        {
            var transactionEntity = HttpContext.Items["transaction"] as Transaction;
            _mapper.Map(transaction, transactionEntity);
            _repository.Transaction.UpdateTransaction(transactionEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateTransactionForAccountExistsAttribute))]
        public async Task<IActionResult> DeleteTransaction(Guid accountId, Guid id)
        {
            var transaction = HttpContext.Items["transaction"] as Transaction;
            _repository.Transaction.DeleteTransaction(transaction);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
