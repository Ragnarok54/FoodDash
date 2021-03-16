namespace FoodDash.Web.Models.Cart
{
    public class CartProductModel
    {
        public int CartProductId { get; set; }

        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } 

        public int ProductPrice { get; set; }

        public int Quantity { get; set; }
    }
}
