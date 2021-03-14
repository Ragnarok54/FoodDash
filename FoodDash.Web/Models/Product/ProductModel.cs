namespace FoodDash.Web.Models.Product
{
    public class ProductModel
    {
        public int RestaurantId { get; set; }
        
        public int ProductId { get; set; }
        
        public int ProductCategoryId { get; set; }
        
        public string ProductName { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int ServingSize { get; set; }

        public bool IsVegetarian { get; set; }
    }
}
