using System.Security.Authentication.ExtendedProtection;
using Microsoft.EntityFrameworkCore;
using OdeToFood.core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
                
        }
        public DbSet<Restaurant> Restaurants { get; set; }   
    }
}