using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using FoodDash.Web.Models.Restaurant;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodDash.Web.Services
{
    public class RestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;

        public RestaurantService(IRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return _restaurantRepository.Get(restaurantId);
        }

        public IEnumerable<RestaurantModel> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();

            foreach (var restaurant in restaurants)
            {
                var deliveryTime = restaurant.DeliveryTime == 0 ? 0 : 100 * restaurant.DeliveryTime / 120;

                yield return new RestaurantModel
                {
                    RestaurantId = restaurant.RestaurantId,
                    Name = restaurant.RestaurantName,
                    //Type = restaurant.ty
                    Description = restaurant.RestaurantDescription,
                    DeliveryTime = restaurant.DeliveryTime,
                    DeliveryTimePercentage = deliveryTime,
                    PhotoBytes = restaurant.Photo
                };
            }
        }

        public void Add(RestaurantModel model)
        {
            var newRestaurant = new Restaurant
            {
                RestaurantName = model.Name,
                RestaurantDescription = model.Description,
                RestaurantTypeId = 1,
                DeliveryTime = model.DeliveryTime
            };

            using (var stream = new StreamReader(model.Photo.OpenReadStream()))
            {
                newRestaurant.Photo = Encoding.ASCII.GetBytes(stream.ReadToEnd());
            }

            _restaurantRepository.Add(newRestaurant);

            _restaurantRepository.SaveChanges();
        }

        public void Edit(RestaurantModel model)
        {
            var restaurant = _restaurantRepository.Get(model.RestaurantId);

            using (var stream = new StreamReader(model.Photo.OpenReadStream()))
            {
                restaurant.Photo = Encoding.ASCII.GetBytes(stream.ReadToEnd());
            }

            restaurant.RestaurantName = model.Name;
            restaurant.RestaurantDescription = model.Description;
            restaurant.RestaurantDescription = model.Description;
            restaurant.DeliveryTime = model.DeliveryTime;
            _restaurantRepository.SaveChanges();
        }

        public void Remove(int restaurantId)
        {
            var restaurant = _restaurantRepository.Get(restaurantId);

            _restaurantRepository.Remove(restaurant);

            _restaurantRepository.SaveChanges();
        }
    }
}
