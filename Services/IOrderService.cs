using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public interface IOrderService
{
    void Create(Order order, OrderCreateViewModel viewModel);
    List<Order> GetAll(string filter);
    List<Order> GetAll();
    void Update(Order obj);
    void Delete(int id);
    Order? GetById(int? id);
    OrderCreateViewModel? Listar(OrderCreateViewModel viewModel);
    List<SelectListItem>? GetAvailableFoods(int menuTypeId);
    List<SelectListItem>? GetAvailableFoods();
    List<SelectListItem>? GetAvailableClients();
    List<SelectListItem>? GetAvailableFoodsEdit();
}
