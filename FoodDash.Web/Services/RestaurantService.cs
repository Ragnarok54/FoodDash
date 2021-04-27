using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.DataAccess.Repository.Concrete;
using FoodDash.Web.DataAccess.Repository.Interfaces;
using FoodDash.Web.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoodDash.Web.Services
{
    public class RestaurantService
    {
        private readonly RestaurantRepository _restaurantRepository;
        private readonly IRepository<RestaurantType> _restaurantTypeRepository;

        public RestaurantService(IRepository<Restaurant> restaurantRepository, IRepository<RestaurantType> restaurantTypeRepository)
        {
            _restaurantRepository = (RestaurantRepository)restaurantRepository;
            _restaurantTypeRepository = restaurantTypeRepository;
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return _restaurantRepository.Get(restaurantId);
        }

        public IEnumerable<RestaurantType> GetRestaurantTypes()
        {
            return _restaurantTypeRepository.GetAll();
        }

        public IEnumerable<RestaurantModel> GetAll()
        {
            var restaurants = _restaurantRepository.GetAllWithType();

            foreach (var restaurant in restaurants)
            {
                var deliveryTime = restaurant.DeliveryTime == 0 ? 0 : Math.Min(100 * restaurant.DeliveryTime / 120, 100);

                yield return new RestaurantModel
                {
                    RestaurantId = restaurant.RestaurantId,
                    Name = restaurant.RestaurantName,
                    TypeId = restaurant.RestaurantTypeId,
                    Type = restaurant.RestaurantType.Name,
                    Description = restaurant.RestaurantDescription,
                    DeliveryTime = restaurant.DeliveryTime,
                    DeliveryTimePercentage = deliveryTime,
                    PhotoBytes = restaurant.Photo
                };
            }
        }

        public Restaurant GetRestaurantWithDetails(int restaurantId)
        {
            return _restaurantRepository.GetWithDetails(restaurantId);
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

            if(model.Photo != null)
            {
                using (var br = new BinaryReader(model.Photo.OpenReadStream()))
                {
                    newRestaurant.Photo = br.ReadBytes((int)model.Photo.Length);
                }
            }

            _restaurantRepository.Add(newRestaurant);

            _restaurantRepository.SaveChanges();
        }

        public void Edit(RestaurantModel model)
        {
            var restaurant = _restaurantRepository.Get(model.RestaurantId);
            if(model.Photo != null)
            {
                using (var br = new BinaryReader(model.Photo.OpenReadStream()))
                {
                    restaurant.Photo = br.ReadBytes((int)model.Photo.Length);
                }
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
