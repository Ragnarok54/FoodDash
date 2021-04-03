using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserRoleId { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
    }
}
