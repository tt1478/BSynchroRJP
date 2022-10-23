using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSynchroRJP.ActionFilters
{
    public class ValidateTransactionForAccountExistsAttribute : IAsyncActionFilter
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateTransactionForAccountExistsAttribute(IloggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accountId = (Guid)context.ActionArguments["accountId"];
            var customerId = context.ActionArguments["customerId"].ToString();
            var account = await _repository.Account.GetAccount(customerId, accountId, false);
            if(account == null)
            {
                _logger.LogInfo($"Company with id: {accountId} doesn't exist in the database."); 
                context.Result = new NotFoundResult(); 
                return;
            }
            var id = (Guid)context.ActionArguments["id"];
            var transaction = await _repository.Transaction.GetTransaction(accountId, id, false);
            if (transaction == null)
            {
                _logger.LogInfo($"Transaction with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("transaction", transaction);
                await next();
            }
        }
    }
}
