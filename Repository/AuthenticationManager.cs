using Contracts;
using Entities.DataTransferObjects.Customer;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<Customer> _userManager; 
        private readonly IConfiguration _configuration; 
        private Customer _customer;
        public AuthenticationManager(UserManager<Customer> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials(); 
            var claims = await GetClaims(); 
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims); 
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(CustomerForAuthenticationDto customerForAuth)
        {
            _customer = await _userManager.FindByNameAsync(customerForAuth.UserName); 
            return (_customer != null && await _userManager.CheckPasswordAsync(_customer, customerForAuth.Password));
        }
        private SigningCredentials GetSigningCredentials() 
        { 
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")); 
            var secret = new SymmetricSecurityKey(key); 
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); 
        }
        private async Task<List<Claim>> GetClaims() 
        { 
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _customer.UserName) }; 
            var roles = await _userManager.GetRolesAsync(_customer); 
            foreach (var role in roles) 
            { 
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            } 
            return claims; 
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) 
        { 
            var jwtSettings = _configuration.GetSection("JwtSettings"); 
            var tokenOptions = new JwtSecurityToken(issuer: jwtSettings.GetSection("validIssuer").Value, 
                                                    audience: jwtSettings.GetSection("validAudience").Value, 
                                                    claims: claims, 
                                                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)), 
                                                    signingCredentials: signingCredentials); 
            return tokenOptions; 
        }
    }
}
