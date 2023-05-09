using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stix.Models;

namespace Stix.Data
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodRestaurant> FoodRestaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodRestaurant>()
                .HasKey(f => new { f.FoodId, f.RestaurantId });

            modelBuilder.Entity<FoodRestaurant>()
                .HasOne(f => f.Food)
                .WithMany(f => f.Restaurants)
                .HasForeignKey(f => f.FoodId);

            modelBuilder.Entity<FoodRestaurant>()
                .HasOne(f => f.Restaurant)
                .WithMany(r => r.Foods)
                .HasForeignKey(f => f.RestaurantId);
        }
    }
}