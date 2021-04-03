using System.ComponentModel.DataAnnotations;

namespace FoodDash.Web.DataAccess.Entities
{
    public class Cart
    {
        [Key]
        public int UserId { get; set; }
        
        [Key]
        public int ProductId { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
