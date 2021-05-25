using FoodDash.Web.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDash.Web.DataAccess.Repository.Concrete
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(Context context) : base(context) { }

        public Order GetCurrentOrder(string userId)
        {
            return _context.Orders.Include(o => o.OrderProducts)
                                  .ThenInclude(op => op.Product)
                                  .ThenInclude(p => p.Restaurant)
                                  .SingleOrDefault(o => o.UserId == userId && o.Date == null);
        }

        public void AddToOrder(int productId, string userId)
        {
            var order = GetCurrentOrder(userId);
            var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);

            if(order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    Price = 0
                };

                _context.Orders.Add(order);
                SaveChanges();
                order = GetCurrentOrder(userId);
            }

            order.Price += product.Price;

            var orderProduct = new OrderProduct
            {
                Order = order,
                Product = product
            };

            order.OrderProducts.Add(orderProduct);

            SaveChanges();
        }

        public void RemoveProduct(int productId, string userId)
        {
            var order = GetCurrentOrder(userId);

            var product = order.OrderProducts.SingleOrDefault(op => op.ProductId == productId);

            order.OrderProducts.Remove(product);

            SaveChanges();
        }
    }
}
