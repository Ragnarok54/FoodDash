using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("Restaurant")]
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        public string RestaurantDescription { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        public int RestaurantTypeId { get; set; }
  
        [Required]
        public virtual RestaurantType RestaurantType { get; set; }

        [Required]
        public int DeliveryTime { get; set; }
        
        [Required]
        public virtual ICollection<Product> Products { get; set; }
    
    }
}
