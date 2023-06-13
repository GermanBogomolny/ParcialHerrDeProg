using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stix.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stix.Data
{
    public class FoodContext : IdentityDbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodRestaurant> FoodRestaurants { get; set; }
        public DbSet<Client> Clients {get;set;}
        public DbSet<Order> Orders {get;set;}

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
                
            modelBuilder.Entity<Client>()
                .HasMany(f => f.Orders)
                .WithOne(f => f.Client);
            base.OnModelCreating(modelBuilder);

        }
    }
}