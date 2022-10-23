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
    public class ValidateAccountExistsAttribute : IAsyncActionFilter
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateAccountExistsAttribute(IloggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments["id"];
            var customerId = context.ActionArguments["customerId"].ToString();
            var account = await _repository.Account.GetAccount(customerId, id, false);
            if (account == null) 
            { 
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database."); 
                context.Result = new NotFoundResult(); 
            } 
            else 
            { 
                context.HttpContext.Items.Add("account", account); 
                await next(); 
            }
        }
    }
}
