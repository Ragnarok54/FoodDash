using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace FoodDash.Web.Models.Restaurant
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }
        
        public string Type { get; set; }

        [DisplayName("Delivery time")]
        public int DeliveryTime { get; set; }

        public int DeliveryTimePercentage { get; set; }

        public IFormFile Photo { get; set; }

        public byte[] PhotoBytes { get; set; }
    }
}
