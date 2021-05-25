using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Concrete;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using FoodDash.Web.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDash.Web.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<OrderProduct> orderProductRepository)
        {
            _orderRepository = (OrderRepository)orderRepository;
            _orderProductRepository = orderProductRepository;
        }


        public void Add(string userId, int productId)
        {
            _orderRepository.AddToOrder(productId, userId);
        }

        public void Remove(int productId, string userId)
        {
            var order = _orderRepository.GetCurrentOrder(userId);

            var product = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);

            order.OrderProducts.Remove(product);
            _orderRepository.SaveChanges();
        }

        public void PlaceOrder(string userId)
        {
            var order = _orderRepository.GetCurrentOrder(userId);

            order.Date = DateTime.Now;

            _orderRepository.SaveChanges();
        }

        public CartModel GetOrderDetails(string userId)
        {
            var order = _orderRepository.GetCurrentOrder(userId);
            if(order == null)
            {
                return new CartModel();
            }

            var products = order.OrderProducts.Select(op => op.Product);

            var productListModel = new List<CartProductModel>();

            foreach (var product in products)
            {
                var productModel = productListModel.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (productModel == null)
                {
                    productModel = new CartProductModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Quantity = 1,
                        RestaurantId = product.RestaurantId,
                        RestaurantName = product.Restaurant.RestaurantName
                    };
                    productListModel.Add(productModel);
                }
                else
                {
                    productModel.Quantity++;
                }
            }

            return new CartModel
            {
                Products = productListModel
            };
        }
    }
}
