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
        public FoodContext (DbContextOptions<FoodContext> options)
            : base(options)
        {
        }

        public DbSet<Stix.Models.Food> Food { get; set; } = default!;
        public DbSet<Stix.Models.Restaurant> Restaurant { get; set; } = default!;
        public DbSet<Stix.Models.FoodRestaurant> FoodRestaurant { get; set; } = default!;

        protected override void OnModelCreating (ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Food>()
            .HasMany(p => p.Restaurant)
            .WithMany(g => g.Food)
            .UsingEntity<FoodRestaurant>(
                /*pg => pg.HasOne(prop => prop.Restaurant)
                .WithMany()
                .HasForeignKey(prop => prop.RestaurantId),
                pg => pg.HasOne(prop => prop.Food)
                .WithMany()
                .HasForeignKey(prop => prop.FoodId),
                pg => {
                    pg.HasKey(prop => new {prop.FoodId, prop.RestaurantId,prop.FoodTypeId});
                }*/
                );
        }
    }
}
