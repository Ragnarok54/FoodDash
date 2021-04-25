﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace FoodDash.Web.Models.Product
{
    public class ProductModel
    {
        public int RestaurantId { get; set; }
        
        public int ProductId { get; set; }
        
        [DisplayName("Product type")]
        public int ProductTypeId { get; set; }
        
        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int ServingSize { get; set; }

        [DisplayName("Is the product vegetarian")]
        public bool IsVegetarian { get; set; }

        public IFormFile Photo { get; set; }
    }
}
//            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Name", product.ProductTypeId);
