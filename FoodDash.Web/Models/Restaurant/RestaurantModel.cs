namespace FoodDash.Web.Models.Restaurant
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int DeliveryTime { get; set; }

        public int DeliveryTimePercentage { get; set; }
    }
}
