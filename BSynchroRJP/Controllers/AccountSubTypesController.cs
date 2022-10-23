using AutoMapper;
using BSynchroRJP.ActionFilters;
using Contracts;
using Entities.DataTransferObjects.AccountSubType;
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
    public class AccountSubTypesController : ControllerBase
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AccountSubTypesController(IloggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountSubTypes()
        {
            var accountSubTypes = await _repository.AccountSubType.GetAccountSubTypes(false);
            var accountSubTypesDto = _mapper.Map<IEnumerable<AccountSubTypeDto>>(accountSubTypes);
            return Ok(accountSubTypesDto);
        }
        [HttpGet("{id}", Name = "GetAccountSubTypeById")]
        public async Task<IActionResult> GetAccountSubType(Guid id)
        {
            var accountSubType = await _repository.AccountSubType.GetAccountSubType(id, false);
            if(accountSubType == null)
            {
                _logger.LogInfo($"Account sub type with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var accountSubTypeDto = _mapper.Map<AccountSubTypeDto>(accountSubType);
            return Ok(accountSubTypeDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccountSubType([FromBody]AccountSubTypeCreationDto accountSubType)
        {
            var accountSubTypeEntity = _mapper.Map<AccountSubType>(accountSubType);
            _repository.AccountSubType.CreateAccountSubType(accountSubTypeEntity);
            await _repository.SaveAsync();
            var accountSubTypeToReturn = _mapper.Map<AccountSubTypeDto>(accountSubTypeEntity);
            return CreatedAtRoute("GetAccountSubTypeById", new { id = accountSubTypeToReturn.Id }, accountSubTypeToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAccountSubTypeExistsAttribute))]
        public async Task<IActionResult> UpdateAccountSubType(Guid id, [FromBody]AccountSubTypeUpdateDto accountSubType)
        {
            var accountSubTypeEntity = HttpContext.Items["accountSubType"] as AccountSubType;
            _mapper.Map(accountSubType, accountSubTypeEntity);
            _repository.AccountSubType.UpdateAccountSubType(accountSubTypeEntity);
            await _repository.SaveAsync();
            return NoContent();
        } 
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAccountSubTypeExistsAttribute))]
        public async Task<IActionResult> DeleteAccountSubType(Guid id)
        {
            var accountSubType = HttpContext.Items["accountSubType"] as AccountSubType;
            _repository.AccountSubType.DeleteAccountSubType(accountSubType);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
