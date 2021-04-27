using FoodDash.Web.Models.Product;
using FoodDash.Web.Models.Restaurant;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var model = new HomeModel
            {
                Restaurants = _restaurantService.GetAll()
            };

            return View("~/Views/Restaurant/Home.cshtml", model);
        }

        [Route("Restaurant")]
        public IActionResult Details(int restaurantId)
        {
            try
            {
                var restaurant = _restaurantService.GetRestaurantWithDetails(restaurantId);

                var model = new RestaurantDetailsModel
                {
                    RestaurantId = restaurantId,
                    Products = restaurant.Products.Select(p => new ProductModel
                    {
                        RestaurantId = p.RestaurantId,
                        ProductId = p.ProductId,
                        Description = p.Description,
                        Name = p.Name,
                        PhotoBytes = p.Photo,
                        Price = p.Price,
                        ProductTypeId = p.ProductTypeId,
                        ServingSize = p.ServingSize,
                        IsVegetarian = p.IsVegetarian
                    }),
                };

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
                var restaurantTypes = _restaurantService.GetRestaurantTypes();

                if (restaurantId > 0)
                {
                    var restaurant = _restaurantService.GetRestaurant(restaurantId);

                    model = new RestaurantModel
                    {
                        RestaurantId = restaurant.RestaurantId,
                        Name = restaurant.RestaurantName,
                        TypeId = restaurant.RestaurantTypeId,
                        Type = restaurantTypes.First(rt => rt.RestaurantTypeId == restaurant.RestaurantTypeId).Name,
                        Description = restaurant.RestaurantDescription,
                        DeliveryTime = restaurant.DeliveryTime,
                        DeliveryTimePercentage = restaurant.DeliveryTime == 0 ? 0 : Math.Min(100 * restaurant.DeliveryTime / 120, 100)
                    };
                }

                ViewData["RestaurantTypeId"] = new SelectList(restaurantTypes, "RestaurantTypeId", "Name", 1);

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
    }
}
