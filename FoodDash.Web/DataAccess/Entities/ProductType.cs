using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
