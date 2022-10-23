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
    public class ValidateAccountSubTypeExistsAttribute : IAsyncActionFilter
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        public ValidateAccountSubTypeExistsAttribute(IloggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments["id"];
            var accountSubType = await _repository.AccountSubType.GetAccountSubType(id, false);
            if (accountSubType == null)
            {
                _logger.LogInfo($"Account sub type with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("accountSubType", accountSubType);
                await next();
            }
        }
    }
}
