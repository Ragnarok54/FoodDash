using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("OrderProduct")]
    public class OrderProduct
    {
        [Required]
        public int OrderProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public virtual Product Product { get; set; }

    }
}
