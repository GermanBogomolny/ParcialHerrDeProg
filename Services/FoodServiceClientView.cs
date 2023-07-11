using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public class FoodServiceClientView : IFoodServiceClientView
{

    private readonly FoodContext _context;

    public FoodServiceClientView(FoodContext context)
    {
        _context = context;
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

}
