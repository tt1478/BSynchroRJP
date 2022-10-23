using AutoMapper;
using BSynchroRJP.ActionFilters;
using Contracts;
using Entities.DataTransferObjects.AccountType;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypesController : ControllerBase
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AccountTypesController(IloggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountTypes()
        {
            var accountTypes = await _repository.AccountType.GetAccountTypes(false);
            var accountTypesDto = _mapper.Map<IEnumerable<AccountTypeDto>>(accountTypes);
            return Ok(accountTypesDto);
        }
        [HttpGet("{id}", Name = "GetAccountTypeById")]
        public async Task<IActionResult> GetAccountType(Guid id)
        {
            var accountType = await _repository.AccountType.GetAccountType(id, false);
            if(accountType == null)
            {
                _logger.LogInfo($"Account type with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var accountTypeDto = _mapper.Map<AccountTypeDto>(accountType);
            return Ok(accountTypeDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccountType([FromBody]AccountTypeCreationDto accountType)
        {
            var accountTypeEntity = _mapper.Map<AccountType>(accountType);
            _repository.AccountType.CreateAccountType(accountTypeEntity);
            await _repository.SaveAsync();
            var accountTypeToReturn = _mapper.Map<AccountTypeDto>(accountTypeEntity);
            return CreatedAtRoute("GetAccountTypeById", new { id = accountTypeToReturn.Id }, accountTypeToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAccountTypeExistsAttribute))]
        public async Task<IActionResult> UpdateAccountType(Guid id, [FromBody]AccountTypeUpdateDto accountType)
        {
            var accountTypeEntity = HttpContext.Items["accountType"] as AccountType;
            _mapper.Map(accountType, accountTypeEntity);
            _repository.AccountType.UpdateAccountType(accountTypeEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAccountTypeExistsAttribute))]
        public async Task<IActionResult> DeleteAccountType(Guid id)
        {
            var accountType = HttpContext.Items["accountType"] as AccountType;
            _repository.AccountType.DeleteAccountType(accountType);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
