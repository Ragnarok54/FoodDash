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

        public Order GetOrderDetails(int userId)
        {
            return _context.Orders.Include(o => o.OrderProducts)
                                  .ThenInclude(op => op.Product)
                                  .ThenInclude(p => p.Restaurant)
                                  .SingleOrDefault(o => o.UserId == userId);
        }

        public void AddToOrder(int productId, int userId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId);
            var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);

            if(order == null)
            {
                order = new Order
                {
                    UserId = userId
                };

                _context.Orders.Add(order);
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
    }
}
