using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.Customer
{
    public class CustomerForAuthenticationDto
    {
        [Required] 
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
