using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;


namespace Stix.Services;

public class ClientService : IClientService
{

    private readonly FoodContext _context;

    public ClientService(FoodContext context)
    {
        _context = context;
    }
    public void Create(Client client, ClientEditViewModel viewModel)
    {
        _context.Add(client);
        _context.SaveChangesAsync();

    }

    public List<Client> GetAll(string filter)
    {
        var query = from client in _context.Clients select client;
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.NameClient.ToLower().Contains(filter.ToLower())||
            x.SurnameClient.ToLower().Contains(filter.ToLower()) ||
            x.EmailClient.ToLower().Contains(filter.ToLower()) || 
            x.PhoneClient.ToString().Contains(filter));
        }
        return query.ToList();
    }
    public void Update(Client obj)
    {
        _context.Update(obj);
        _context.SaveChangesAsync();
    }


    public void Delete(int id)
    {
        var client = _context.Foods.Find(id);
        if (client != null)
        {
            _context.Foods.Remove(client);
        }

        _context.SaveChangesAsync();

    }

    public Client GetById(int? id)
    {
        var client = _context.Clients.Find(id);
        return client;
    }
}
