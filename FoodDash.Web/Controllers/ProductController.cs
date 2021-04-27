using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.Models.Product;
using FoodDash.Web.Models.Restaurant;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoodDash.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly ProductService _productService;

        public ProductController(ILogger<RestaurantController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Add(int restaurantId)
        {
            try
            {
                var model = new ProductModel
                {
                    RestaurantId = restaurantId,
                    ProductTypeId = 1
                };

                var productTypes = _productService.GetProductTypes();
                ViewData["ProductTypeId"] = new SelectList(productTypes, "ProductTypeId", "Name", 1);

                return View("~/Views/Product/ProductEdit.cshtml", model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Index", "Restaurant");
            }
        }

        [HttpGet]
        public IActionResult Edit(int productId)
        {
            try
            {
                var product = _productService.GetProduct(productId);
                var model = new ProductModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    RestaurantId = product.RestaurantId,
                    ServingSize = product.ServingSize,
                    ProductTypeId = product.ProductTypeId,
                    IsVegetarian = product.IsVegetarian
                };

                var productTypes = _productService.GetProductTypes();
                ViewData["ProductTypeId"] = new SelectList(productTypes, "ProductTypeId", "Name", product.ProductTypeId);

                return View("~/Views/Product/ProductEdit.cshtml", model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Index", "Restaurant");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            try
            {

                var product = new Product
                {
                    RestaurantId = model.RestaurantId,
                    ProductId = model.ProductId,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductTypeId = model.ProductTypeId,
                    ServingSize = model.ServingSize,
                    IsVegetarian = model.IsVegetarian
                };

                if (model.Photo != null)
                {
                    using (var br = new BinaryReader(model.Photo.OpenReadStream()))
                    {
                        product.Photo = br.ReadBytes((int)model.Photo.Length);
                    }
                }

                if (product.ProductId > 0)
                {
                    _productService.Edit(product);
                }
                else
                {
                    _productService.Add(product);
                }

                return RedirectToAction("Details", "Restaurant", new { restaurantId = model.RestaurantId});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Index", "Restaurant");
            }
        }
    }
}
