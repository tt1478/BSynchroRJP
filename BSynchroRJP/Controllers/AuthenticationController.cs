using AutoMapper;
using BSynchroRJP.ActionFilters;
using Contracts;
using Entities.DataTransferObjects.Customer;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSynchroRJP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IloggerManager _logger; 
        private readonly IMapper _mapper; 
        private readonly UserManager<Customer> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationController(IloggerManager logger, IMapper mapper, UserManager<Customer> userManager, IAuthenticationManager authenticationManager) 
        { 
            _logger = logger; 
            _mapper = mapper; 
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
        [HttpPost("register")] 
        [ServiceFilter(typeof(ValidationFilterAttribute))] 
        public async Task<IActionResult> RegisterUser([FromBody]CustomerForRegistrationDto customerForRegistration) 
        { 
            var user = _mapper.Map<Customer>(customerForRegistration); 
            var result = await _userManager.CreateAsync(user, customerForRegistration.Password); 
            if (!result.Succeeded) 
            { 
                foreach (var error in result.Errors) 
                { 
                    ModelState.TryAddModelError(error.Code, error.Description); 
                } 
                return BadRequest(ModelState); 
            } 
            await _userManager.AddToRolesAsync(user, customerForRegistration.Roles); 
            return StatusCode(201); 
        }
        [HttpPost("login")] 
        [ServiceFilter(typeof(ValidationFilterAttribute))] 
        public async Task<IActionResult> Authenticate([FromBody] CustomerForAuthenticationDto customer) 
        { 
            if (!await _authenticationManager.ValidateUser(customer)) 
            { 
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password."); 
                return Unauthorized(); 
            }
            var user = await _userManager.FindByNameAsync(customer.UserName);
            return Ok(new { UserName = user.UserName, Token = await _authenticationManager.CreateToken(), UserId = user.Id }); 
        }
    }
}
