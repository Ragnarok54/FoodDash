using System.Collections.Generic;

namespace FoodDash.Web.Models.Restaurant
{
    public class HomeModel
    {
        public IEnumerable<RestaurantModel> Restaurants { get; set; }
    }
}
