using FoodDash.Web.Models.Account;
using FoodDash.Web.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FoodDash.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new CartModel
            {
                Products = new List<CartProductModel>
                {
                    new CartProductModel
                    {
                        CartProductId = 1,
                        RestaurantId = 1,
                        RestaurantName = "KFC",
                        ProductId = 1,
                        Quantity = 1,
                        ProductName = "Meniu 5 Crispy Strips",
                        ProductPrice = 28,
                    },
                    new CartProductModel
                    {
                        CartProductId = 3,
                        RestaurantId = 1,
                        RestaurantName = "KFC",
                        ProductId = 3,
                        Quantity = 2,
                        ProductName = "Meniu 8 Crispy Strips",
                        ProductPrice = 32,
                    },
                    new CartProductModel
                    {
                        CartProductId = 3,
                        RestaurantId = 1,
                        RestaurantName = "KFC",
                        ProductId = 2,
                        Quantity = 1,
                        ProductName = "Meniu 5 Hot Wings",
                        ProductPrice = 28,
                    }
                }
            };

            return View("~/Views/Cart/Cart.cshtml", model);
        }
        
    }
}
