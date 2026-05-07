using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
