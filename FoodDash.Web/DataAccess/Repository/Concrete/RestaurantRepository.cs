using FoodDash.Web.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDash.Web.DataAccess.Repository.Concrete
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(Context context) : base(context) { }

        public Restaurant GetWithType(int restaurantId)
        {
            return _context.Restaurants.Include(r => r.RestaurantType).FirstOrDefault(r => r.RestaurantId == restaurantId);
        }

        public IEnumerable<Restaurant> GetAllWithType()
        {
            return _context.Restaurants.Include(r => r.RestaurantType);
        }

        public Restaurant GetWithDetails(int restaurantId)
        {
            return _context.Restaurants.Include(r => r.Products).FirstOrDefault(r => r.RestaurantId == restaurantId);
        }
    }
}
