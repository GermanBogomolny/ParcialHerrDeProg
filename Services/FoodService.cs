using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public class FoodService : IFoodService
{

    private readonly FoodContext _context;

    public FoodService(FoodContext context)
    {
        _context = context;
    }
    public void Create(Food food, FoodCreateViewModel viewModel)
    {
        _context.Add(food);
        _context.SaveChangesAsync();

    }

    public void Delete(int id)
    {
        var food = _context.Foods.Find(id);
        if (food != null)
        {
            _context.Foods.Remove(food);
        }

        _context.SaveChangesAsync();

    }

    public List<Food> GetAll(string filter)
    {
        var query = from food in _context.Foods select food;
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.NameFood.ToLower().Contains(filter.ToLower()) ||
            x.DescriptionFood.Contains(filter.ToLower()) ||
            x.Price.ToString() == filter);
        }


        return query.ToList();
    }

    public Food GetById(int? id)
    {
        var food = _context.Foods.Find(id);

        return food;
    }

    public void Update(Food obj)
    {
        _context.Update(obj);
        _context.SaveChangesAsync();
    }
    public bool FoodExists(int id)
    {
        return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
