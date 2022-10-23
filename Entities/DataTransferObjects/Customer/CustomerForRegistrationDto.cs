using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.Customer
{
    public class CustomerForRegistrationDto
    {
        [Required]
        [MinLength(5)]
        public string FullName { get; set; }
        [Required] 
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
