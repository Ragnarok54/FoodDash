using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDash.Web.DataAccess.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User {get; set;}

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}
