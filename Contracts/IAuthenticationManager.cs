using Entities.DataTransferObjects.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(CustomerForAuthenticationDto customerForAuth); 
        Task<string> CreateToken();
    }
}
