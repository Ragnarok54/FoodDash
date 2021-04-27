using FoodDash.Web.DataAccess;
using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Concrete;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FoodDash.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));

            // Add repositories
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();
            services.AddScoped<IRepository<RestaurantType>, Repository<RestaurantType>>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<ProductType>, Repository<ProductType>>();

            // Add services
            services.AddScoped<RestaurantService>();
            services.AddScoped<OrderService>();
            services.AddScoped<ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
