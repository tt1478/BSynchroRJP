using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Customer:IdentityUser
    {
        [Required]
        [MinLength(5)]
        public string FullName { get; set; }
        
    }
}
