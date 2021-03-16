
using System.Collections.Generic;

namespace FoodDash.Web.Models.Cart
{
    public class CartModel
    {
        public IEnumerable<CartProductModel> Products { get; set; }
    }
}
