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
    public class ValidateAccountStatusExistsAttribute : IAsyncActionFilter
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateAccountStatusExistsAttribute(IloggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments["id"];
            var accountStatus = await _repository.AccountStatus.GetAccountStatus(id, false);
            if(accountStatus == null)
            {
                _logger.LogInfo($"Account status with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("accountStatus", accountStatus);
                await next();
            }
        }
    }
}
