using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        public byte[] Photo { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int ServingSize { get; set; }

        [Required]
        public bool IsVegetarian { get; set; }
    }
}
