using Microsoft.EntityFrameworkCore;
using Restaurants.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var Restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    ContactNumber = "696969699",
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                            Description = "Spicy"
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                            Description = "Ultra BIG"
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonalds",
                    Category = "Fast Food",
                    Description = "McDonald’s Corporation – amerykańska sieć barów szybkiej obsługi, sprzedająca głównie burgery, frytki i napoje.",
                    ContactEmail = "contact@mc.com",
                    HasDelivery = true,
                    ContactNumber = "292934949",
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "McRoyal",
                            Price = 15.30M,
                            Description = "With cheese"
                        },
                        new Dish()
                        {
                            Name = "Mc Nuggets",
                            Price = 7.25M,
                            Description = "small"
                        },
                    },
                    Address = new Address()
                    {
                        City = "Rzeszów",
                        Street = "Lwowska",
                        PostalCode = "35-301"
                    }
                }
            };
            return Restaurants;
        }
    }
}
