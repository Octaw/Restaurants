using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurants.Entities;
using Restaurants.Exceptions;
using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Services
{
    public interface IDishService
    {
        int Create(int restaurantIdm, CreateDishDto dto);
        DishDto GetById(int restaurantId, int DishId);
        List<DishDto> GetAll(int restaurantId);
        void RemoveAll(int restaurantId);
    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishentity = _mapper.Map<Dish>(dto);
            dishentity.RestaurantId = restaurantId;
            _context.Dishes.Add(dishentity);
            _context.SaveChanges();
            return dishentity.Id;
        }
        public DishDto GetById(int restaurantId, int DishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == DishId);
            if(dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundExceptions("Dish not found");
            }
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return dishDtos;
        }
        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }
        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant is null)
            {
                throw new NotFoundExceptions("Restaurant not found");
            }
            return restaurant;
        }
    }
}
