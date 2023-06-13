using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public interface IRestaurantService
{
    void Create(Restaurant restaurant, RestaurantCreateViewModel viewModel);
    List<Restaurant> GetAll(string filter);
    List<FoodRestaurant> GetAllFoodByRestaurantId(int id);
    void Update(Restaurant obj);
    void Delete(Restaurant obj);
    void FoodRestaurantRemoveRange(List<FoodRestaurant> obj);
    Restaurant? GetById(int? id);
    RestaurantCreateViewModel? Listar(RestaurantCreateViewModel viewModel);
    List<SelectListItem>? GetAvailableFoods(int menuTypeId);
    List<SelectListItem>? ListarRestaurantsFoods(Restaurant restaurant);
    List<SelectListItem>? GetAvailableFoodsEdit();
    bool RestaurantExists(int id);

}
