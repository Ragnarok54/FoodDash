using FoodDash.Web.Models.Product;
using FoodDash.Web.Models.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FoodDash.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        [Route("Restaurants")]
        public IActionResult Index()
        {
            var model = new HomeModel();
            var restaurants = new List<RestaurantModel>
            {
                new RestaurantModel
                {
                    RestaurantId = 1,
                    Name = "KFC",
                    Description = "Mancare buna bomba rupem. Hai acilea",
                    Type = "Fast Food",
                    DeliveryTimePercentage = 20
                },
                new RestaurantModel
                {
                    RestaurantId = 2,
                    Name = "KFC",
                    Description = "Mancare buna bomba rupem. Hai acilea",
                    Type = "Restaurant",
                    DeliveryTimePercentage = 90
                }
            };

            model.Restaurants = restaurants;

            return View("~/Views/Restaurant/Home.cshtml", model);
        }

        [Route("Restaurant")]
        public IActionResult Details(int restaurantId)
        {
            try
            {
                var model = new RestaurantDetailsModel();
                var products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        ProductName = "Meniu 5 crispy strips",
                        Description = "5 crispy strips",
                        IsVegetarian = false,
                        RestaurantId = 2,
                        ServingSize = 300,
                        Price = 28
                    },
                    new ProductModel
                    {
                        Description = "5 crispy strips",
                        IsVegetarian = false,
                        RestaurantId = 2,
                        ServingSize = 300,
                        Price = 28
                    },
                    new ProductModel
                    {
                        Description = "5 crispy strips",
                        IsVegetarian = false,
                        RestaurantId = 2,
                        ServingSize = 300,
                        Price = 28
                    },
                    new ProductModel
                    {
                        Description = "5 crispy strips",
                        IsVegetarian = true,
                        RestaurantId = 2,
                        ServingSize = 300,
                        Price = 28
                    },
                    new ProductModel
                    {
                        Description = "5 crispy strips",
                        IsVegetarian = false,
                        RestaurantId = 2,
                        ServingSize = 300,
                        Price = 28
                    },
                };

                model.Products = products;

                return View("~/Views/Restaurant/RestaurantDetails.cshtml", model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to load retaurant details");
                return RedirectToAction("Index");
            }
        }
    }
}
