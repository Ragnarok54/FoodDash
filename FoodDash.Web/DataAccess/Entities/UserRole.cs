using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
