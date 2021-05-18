using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FoodDash.Web.Models.Account
{
    public class ProfileModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public IFormFile Photo { get; set; }
    }
}
