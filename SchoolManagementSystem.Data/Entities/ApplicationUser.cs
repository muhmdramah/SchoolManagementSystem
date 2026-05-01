using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagementSystem.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(maximumLength: 32)]
        public string FirstName { get; set; }

        [Required, StringLength(maximumLength: 32)]
        public string LastName { get; set; }
    }
}
