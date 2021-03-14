using FoodDash.BusinessLogic.DTO;
using FoodDash.Web.Models.Product;
using System.Collections.Generic;

namespace FoodDash.Web.Models.Restaurant
{
    public class RestaurantDetailsModel : RestaurantModel
    {
        public IEnumerable<ProductModel> Products { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
