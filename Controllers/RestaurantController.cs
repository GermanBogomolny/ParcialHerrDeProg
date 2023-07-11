using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.Utils;
using Stix.ViewModels;
using Stix.Services;
using Microsoft.AspNetCore.Authorization;

namespace Stix.Controllers
{
    [Authorize(Roles = "AdminUsuarios, RestaurantManager")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantservice)
        {
            _restaurantService = restaurantservice;
        }


        // GET: Restaurant
        public async Task<IActionResult> Index(string NameFilter)
        {

            var model = new RestaurantViewModel();
            model.Restaurants = _restaurantService.GetAll(NameFilter);

            return View(model);
        }


        public async Task<IActionResult> Create(RestaurantCreateViewModel viewModel)
        {
            var model = new RestaurantCreateViewModel
            {
                MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                                  .Cast<MenuTypeEnum>()
                                  .Select(e => new SelectListItem
                                  {
                                      Text = e.ToString(),
                                      Value = ((int)e).ToString()
                                  }).ToList(),
                AvailableFoods = new List<SelectListItem>()
            };

            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant
                {
                    RestaurantName = viewModel.RestaurantName,
                    Street = viewModel.Street,
                    Number = viewModel.Number,
                    Neighbourhood = viewModel.Neighbourhood,
                    Town = viewModel.Town,
                    Provincia = viewModel.Provincia,
                    MenuTypeId = viewModel.MenuTypeId,
                    Foods = new List<FoodRestaurant>()
                };
                _restaurantService.Create(restaurant, viewModel);
                return RedirectToAction(nameof(Index));
            }

            viewModel.MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                .Cast<MenuTypeEnum>()
                .Select(m => new SelectListItem { Value = ((int)m).ToString(), Text = m.ToString() })
                .ToList();

            viewModel = _restaurantService.Listar(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetAvailableFoods(int menuTypeId)
        {
            var availableFoods = _restaurantService.GetAvailableFoods(menuTypeId);

            return Json(availableFoods);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var restaurant = _restaurantService.GetById(id.Value);

            if (restaurant == null)
            {
                return NotFound();
            }

            var viewModel = new RestaurantEditViewModel
            {
                RestaurantName = restaurant.RestaurantName,
                Street = restaurant.Street,
                Number = restaurant.Number,
                Neighbourhood = restaurant.Neighbourhood,
                Town = restaurant.Town,
                Provincia = restaurant.Provincia,
                MenuTypeId = restaurant.MenuTypeId,
                SelectedFoodIds = restaurant.Foods?.Select(f => f.FoodId)?.ToList() ?? new List<int>(),
                MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                               .Cast<MenuTypeEnum>()
                               .Select(e => new SelectListItem
                               {
                                   Text = e.ToString(),
                                   Value = ((int)e).ToString()
                               }).ToList(),
                AvailableFoods = _restaurantService.ListarRestaurantsFoods(restaurant)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantEditViewModel viewModel)
        {

            ModelState.Remove("availableFoods");
            ModelState.Remove("MenuTypes");
            ModelState.Remove("Foods");
            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _restaurantService.GetById(id);
                    if (restaurant == null)
                    {
                        return NotFound();
                    }
                    restaurant.RestaurantName = viewModel.RestaurantName;
                    restaurant.Street = viewModel.Street;
                    restaurant.Number = viewModel.Number;
                    restaurant.Neighbourhood = viewModel.Neighbourhood;
                    restaurant.Town = viewModel.Town;
                    restaurant.Provincia = viewModel.Provincia;
                    restaurant.MenuTypeId = viewModel.MenuTypeId;

                    if (viewModel.SelectedFoodIds != null)
                    {
                        restaurant.Foods = viewModel.SelectedFoodIds.Select(id => new FoodRestaurant
                        {
                            RestaurantId = restaurant.Id,
                            FoodId = id
                        }).ToList();
                    }
                    else
                    {
                        restaurant.Foods = new List<FoodRestaurant>();
                    }
                    _restaurantService.Update(restaurant);
                }
                catch (Exception ex)
                {
                    if (!RestaurantExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; //lanzar alerta con la excepción;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                        .Cast<MenuTypeEnum>()
                        .Select(e => new SelectListItem
                        {
                            Text = e.ToString(),
                            Value = ((int)e).ToString()
                        }).ToList();

            viewModel.AvailableFoods = _restaurantService.GetAvailableFoodsEdit();

            return View(viewModel);
        }

        //Restaurant/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _restaurantService.GetById(id.Value);

            if (restaurant == null)
            {
                return NotFound();
            }

            try
            {
                //Elimina las relaciones de FoodRestaurant
                var foodRestaurants = _restaurantService.GetAllFoodByRestaurantId(id.Value);
                _restaurantService.FoodRestaurantRemoveRange(foodRestaurants);

                //Elimina el restaurante
                _restaurantService.Delete(restaurant);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        //POST: Restaurant/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = _restaurantService.GetById(id);


            if (restaurant == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        private bool RestaurantExists(int id)
        {
            return _restaurantService.RestaurantExists(id);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = _restaurantService.GetById(id.Value);


            if (restaurant == null)
            {
                return NotFound();
            }

            var viewModel = new RestaurantDetailsViewModel
            {
                Restaurant = restaurant,
                RestaurantName = restaurant.RestaurantName,
                Street = restaurant.Street,
                Number = restaurant.Number,
                Neighbourhood = restaurant.Neighbourhood,
                Town = restaurant.Town,
                Provincia = restaurant.Provincia,
                MenuTypeId = restaurant.MenuTypeId,
                AllFoods = restaurant.Foods.Select(fr => new SelectListItem
                {
                    Value = fr.FoodId.ToString(),
                    Text = fr.Food.NameFood
                }).ToList()
            };


            return View(viewModel);
        }
    }
}
