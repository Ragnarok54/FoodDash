using FoodDash.Web.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDash.Web.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>().HasKey(t => new { t.UserId, t.ProductId });
        }
    }
}
