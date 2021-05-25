using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.Models.Account;
using FoodDash.Web.Models.Cart;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace FoodDash.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;
        private readonly IToastNotification _notyf;

        public OrderController(ILogger<OrderController> logger, OrderService orderService, IToastNotification notyf)
        {
            _logger = logger;
            _orderService = orderService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _orderService.GetOrderDetails(currentUserId);

            return View("~/Views/Cart/Cart.cshtml", model);
        }
        
        public IActionResult Add(int productId, int restaurantId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _orderService.Add(currentUserId, productId);
                _notyf.AddSuccessToastMessage("Product added to cart");

                return RedirectToAction("Details", "Restaurant", new { restaurantId = restaurantId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add item to cart failed");
                return RedirectToAction("Details", "Restaurant", new { restaurantId = restaurantId});
            }
        }

        public IActionResult Remove(int productId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _orderService.Remove(productId, currentUserId);

                _notyf.AddSuccessToastMessage("Product removed from cart");

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Remove item from cart failed");
                return RedirectToAction("Index");
            }
        }
    }
}
