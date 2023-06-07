using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Viewmodels;

namespace Stix.Services;

public class RestaurantService : IRestaurantService
{

    private readonly FoodContext _context;

    public RestaurantService(FoodContext context)
    {
        _context = context;
    }
    public void Create(Restaurant obj)
    {
        _context.Restaurants.Add(obj);
        _context.SaveChanges();

        throw new NotImplementedException();
    }

    public void Delete(Restaurant obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public List<Restaurant> GetAll(string filter)
    {
            var query = from restaurant in _context.Restaurants select restaurant;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.RestaurantName.ToLower().Contains(filter.ToLower()) ||
                x.Street.ToLower().Contains(filter.ToLower()) ||
                x.Neighbourhood.ToLower().Contains(filter.ToLower()) ||
                x.Town.ToLower().Contains(filter.ToLower()) ||
                x.Provincia.ToLower().Contains(filter.ToLower()));
            }
            return query.ToList();
    }

    public Restaurant GetById(int id)
    {
        var restaurant = _context.Restaurants
        .Include(r => r.Foods)
        .ThenInclude(fr => fr.Food)
        .FirstOrDefault(m => m.Id == id);

        return restaurant;
    }

    public void Update(Restaurant obj)
    {
            _context.Update(obj);
            _context.SaveChanges();
    }
}
