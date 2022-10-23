using AutoMapper;
using BSynchroRJP.ActionFilters;
using Contracts;
using Entities.DataTransferObjects.AccountStatus;
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
    public class AccountStatusesController : ControllerBase
    {
        private readonly IloggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AccountStatusesController(IloggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountStatus()
        {
            var accountStatuses = await _repository.AccountStatus.GetAccountStatuses(false);
            var accountStatusesDto = _mapper.Map<IEnumerable<AccountStatusDto>>(accountStatuses);
            return Ok(accountStatusesDto);
        }
        [HttpGet("{id}", Name = "GetAccountStatusById")]
        public async Task<IActionResult> GetAccountStatus(Guid id)
        {
            var accountStatus = await _repository.AccountStatus.GetAccountStatus(id, false);
            if(accountStatus == null)
            {
                _logger.LogInfo($"Account status with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var accountStatusDto = _mapper.Map<AccountStatusDto>(accountStatus);
            return Ok(accountStatusDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccountStatus([FromBody]AccountStatusCreationDto accountStatus)
        {
            var accountStatusEntity = _mapper.Map<AccountStatus>(accountStatus);
            _repository.AccountStatus.CreateAccountStatus(accountStatusEntity);
            await _repository.SaveAsync();
            var accountStatusToReturn = _mapper.Map<AccountStatusDto>(accountStatusEntity);
            return CreatedAtRoute("GetAccountStatusById", new { id = accountStatusToReturn.Id }, accountStatusToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAccountStatusExistsAttribute))]
        public async Task<IActionResult> UpdateAccountStatus(Guid id, [FromBody]AccountStatusUpdateDto accountStatus)
        {
            var accountStatusEntity = HttpContext.Items["accountStatus"] as AccountStatus;
            _mapper.Map(accountStatus, accountStatusEntity);
            _repository.AccountStatus.UpdateAccountStatus(accountStatusEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAccountStatusExistsAttribute))]
        public async Task<IActionResult> DeleteAccountStatus(Guid id)
        {
            var accountStatus = HttpContext.Items["accountStatus"] as AccountStatus;
            _repository.AccountStatus.DeleteAccountStatus(accountStatus);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
