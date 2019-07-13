using System;
using System.Collections.Generic;
using OdeToFood.core;
using System.Linq;
using Remotion.Linq.Clauses;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
        int GetCountOfRestaurants();
        int Commit();
    }

    public class SqlServerRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;
        public SqlServerRestaurantData(OdeToFoodDbContext dbContext)
        {
            this._context = dbContext;
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in _context.Restaurants
                where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                select r;
            return query;


        }

        public Restaurant GetById(int id)
        {
            return _context.Restaurants.Find(id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _context.Restaurants.Attach(updatedRestaurant);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedRestaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant !=null)
            {
                _context.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return _context.Restaurants.Count();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>() {
                new Restaurant{Id=1,Name="peter's Pizza", Location="Sandy Bay" ,Cuinine=CuisineType.Italian},
                new Restaurant{Id=2,Name="Sichuan restaurant", Location="King Street" ,Cuinine=CuisineType.Chinese},
                new Restaurant{Id=3,Name="HotDog King", Location="Queen Street" ,Cuinine=CuisineType.Mexican},
                new Restaurant{Id=4,Name="Indian Pie", Location="Murray street" ,Cuinine=CuisineType.Indian}

            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant !=null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuinine = updatedRestaurant.Cuinine;

            }

            return restaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
           restaurants.Add(newRestaurant);
           newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
           return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name=null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name select r;
        }
    }
}
