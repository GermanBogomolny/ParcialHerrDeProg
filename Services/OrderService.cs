using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public class OrderService : IOrderService
{

    private readonly FoodContext _context;

    public OrderService(FoodContext context)
    {
        _context = context;
    }
    public void Create(Order order, OrderCreateViewModel viewModel)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();

    }

    public void Delete(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }

        _context.SaveChangesAsync();

    }

    public List<Order> GetAll(string filter)
    {
        var query = from order in _context.Orders select order;
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.NameFood.ToLower().Contains(filter.ToLower()) ||
            x.Qantity.ToString().Contains(filter.ToLower()) ||
            x.DescriptionFood.ToLower().Contains(filter.ToLower()));
        }

        return query.ToList();
    }
    public List<Order> GetAll()
    {
        var query = from order in _context.Orders select order;

        return query.ToList();
    }

    public Order GetById(int? id)
    {
        var order = _context.Orders.Find(id);

        return order;
    }

    public OrderCreateViewModel Listar (OrderCreateViewModel viewModel)
    {
        viewModel.AvailableFoods = _context.Foods
             .Where(f => f.FoodTypeId == viewModel.MenuTypeId)
             .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
             .ToList();

        viewModel.AvailableClients = _context.Clients
             .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameClient })
             .ToList();

        return viewModel;
    }
    public void Update(Order obj)
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
        public List<SelectListItem>? GetAvailableFoods()
    {
        var availableFoods = _context.Foods
            .Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.NameFood
            })
            .ToList();

        return availableFoods;
    }
        public List<SelectListItem>? GetAvailableClients()
    {
        var availableClients = _context.Clients
            .Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.NameClient
            })
            .ToList();

        return availableClients;
    }
    public List<SelectListItem>? GetAvailableFoodsEdit()
    {
        var AvailableFoods = _context.Foods
            .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
            .ToList();

        return AvailableFoods;
    }
}
