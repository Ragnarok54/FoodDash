using FoodDash.Web.Models.Product;
using FoodDash.Web.Models.Restaurant;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoodDash.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly RestaurantService _restaurantService;

        public RestaurantController(ILogger<RestaurantController> logger, RestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        [Route("Restaurants")]
        public IActionResult Index()
        {
            var model = new HomeModel();
            model.Restaurants = _restaurantService.GetAll();

            return View("~/Views/Restaurant/Home.cshtml", model);
        }

        [Route("Restaurant")]
        public IActionResult Details(int restaurantId)
        {
            try
            {
                var model = new RestaurantDetailsModel
                {
                    RestaurantId = restaurantId,
                };
                var products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Meniu 5 crispy strips",
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

        [HttpGet]
        public IActionResult Edit(int restaurantId)
        {
            try
            {
                var model = new RestaurantModel();

                if (restaurantId > 0)
                {
                    var restaurant = _restaurantService.GetRestaurant(restaurantId);

                    model = new RestaurantModel
                    {
                        RestaurantId = restaurant.RestaurantId,
                        Name = restaurant.RestaurantName,
                        Description = restaurant.RestaurantDescription,
                        DeliveryTime = restaurant.DeliveryTime,
                        DeliveryTimePercentage = restaurant.DeliveryTime == 0 ? 0 : 100 * restaurant.DeliveryTime / 120
                    };
                }

                return View("~/Views/Restaurant/RestaurantEdit.cshtml", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load retaurant details");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(RestaurantModel model)
        {
            try
            {
                if (model.RestaurantId > 0)
                {
                    _restaurantService.Edit(model);
                }
                else
                {
                    _restaurantService.Add(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load retaurant details");
                return RedirectToAction("Edit", model.RestaurantId);
            }
        }

        public IActionResult Delete(int restaurantId)
        {
            try
            {
                _restaurantService.Remove(restaurantId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete retaurant with id " + restaurantId);
                return RedirectToAction("Edit", restaurantId);
            }
        }

        public IActionResult GetPhoto(int restaurantId)
        {
            try
            {
                var restaurant = _restaurantService.GetRestaurant(restaurantId);

                return File(restaurant.Photo, "image/jpg");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to get picture for restaurant with id " + restaurantId);
                return RedirectToAction("Index");
            }
        }
    }
}
