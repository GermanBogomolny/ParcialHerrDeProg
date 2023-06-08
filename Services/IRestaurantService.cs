using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public interface IRestaurantService
{
    void Create(Restaurant restaurant, RestaurantCreateViewModel viewModel);
    List<Restaurant> GetAll(string filter);
    void Update(Restaurant obj);
    void Delete(Restaurant obj);
    Restaurant? GetById(int? id);
    RestaurantCreateViewModel? Listar(RestaurantCreateViewModel viewModel);
    List<SelectListItem>? GetAvailableFoods(int menuTypeId);
    List<SelectListItem>? ListarRestaurantsFoods(Restaurant restaurant);
}
