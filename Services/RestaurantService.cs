using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public class RestaurantService : IRestaurantService
{

    private readonly FoodContext _context;

    public RestaurantService(FoodContext context)
    {
        _context = context;
    }
    public void Create(Restaurant restaurant, RestaurantCreateViewModel viewModel)
    {
        foreach (var foodId in viewModel.SelectedFoodIds)
        {
            var food = _context.Foods.Find(foodId);
            if (food != null)
            {
                restaurant.Foods.Add(new FoodRestaurant { Food = food });
            }
        }
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();

    }

    public void Delete(Restaurant obj)
    {
        _context.Restaurants.Remove(obj);
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

    public Restaurant GetById(int? id)
    {
        var restaurant = _context.Restaurants
        .Include(r => r.Foods)
        .ThenInclude(fr => fr.Food)
        .FirstOrDefault(m => m.Id == id);

        return restaurant;
    }

    public RestaurantCreateViewModel Listar(RestaurantCreateViewModel viewModel)
    {
        viewModel.AvailableFoods = _context.Foods
             .Where(f => f.FoodTypeId == viewModel.MenuTypeId)
             .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
             .ToList();

        return viewModel;
    }
    public List<SelectListItem>? ListarRestaurantsFoods(Restaurant restaurant)
    {
        var AvailableFoods = _context.Foods
            .Where(f => f.FoodTypeId == restaurant.MenuTypeId)
            .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
            .ToList();
        return AvailableFoods;
    }
    public void Update(Restaurant obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }

    public List<SelectListItem>? GetAvailableFoods(int menuTypeId)
    {
        var availableFoods = _context.Foods
            .Where(f => f.FoodTypeId == (MenuTypeEnum)menuTypeId)
            .Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.NameFood
            })
            .ToList();

        return availableFoods;
    }
    public List<SelectListItem>? GetAvailableFoodsEdit()
    {
        var AvailableFoods = _context.Foods
            .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
            .ToList();

        return AvailableFoods;
    }

    public List<FoodRestaurant> GetAllFoodByRestaurantId(int id)
    {
        var foodRestaurants = _context.FoodRestaurants.Where(fr => fr.RestaurantId == id).ToList();

        return foodRestaurants;
    }


    public bool RestaurantExists(int id)
    {
        return _context.Restaurants.Any(e => e.Id == id);
    }

    public void FoodRestaurantRemoveRange(List<FoodRestaurant> obj)
    {
        _context.FoodRestaurants.RemoveRange(obj);
    }
}
