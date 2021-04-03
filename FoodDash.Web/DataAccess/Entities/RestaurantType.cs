using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("RestaurantType")]
    public class RestaurantType
    {
        [Key]
        public int RestaurantTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
