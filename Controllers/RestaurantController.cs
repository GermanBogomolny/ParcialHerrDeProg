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
using Viewmodels;

namespace Stix.Controllers
{
    public class RestaurantController : Controller
    {
        //TODO eliminar la siguiente línea
        private readonly FoodContext _context;

        //TODO eliminar context de adentro del constructor
        public RestaurantController(FoodContext context)
        {
            //TODOeliminar la siguiente línea
            _context = context;
        }


        // GET: Restaurant
        public async Task<IActionResult> Index(string NameFilter)
        {

            //TODO eliminar el siguiente código
            var query = from restaurant in _context.Restaurants select restaurant;
            if (!string.IsNullOrEmpty(NameFilter))
            {
                query = query.Where(x => x.RestaurantName.ToLower().Contains(NameFilter.ToLower()) ||
                x.Street.ToLower().Contains(NameFilter.ToLower()) ||
                x.Neighbourhood.ToLower().Contains(NameFilter.ToLower()) ||
                x.Town.ToLower().Contains(NameFilter.ToLower()) ||
                x.Provincia.ToLower().Contains(NameFilter.ToLower()));
            }
            //TODO eliminar hasta aquí

            var model = new RestaurantViewModel();

            //TODO elminar la siguiente línea
            model.Restaurants = await query.ToListAsync();
            //TODO reemplazarla por: model.Restaurants = _restaurantService.GetAll(NameFilter);

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
                foreach (var foodId in viewModel.SelectedFoodIds)
                {
                    var food = await _context.Foods.FindAsync(foodId);
                    if (food != null)
                    {
                        restaurant.Foods.Add(new FoodRestaurant { Food = food });
                    }
                }

                //TODO eliminar este código cuando se haga la inyección de dependencia
                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();
                //TODO reemplazar por:
                //TODO _restaurantService.Create(restaurant);
                return RedirectToAction(nameof(Index));
            }

            viewModel.MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                .Cast<MenuTypeEnum>()
                .Select(m => new SelectListItem { Value = ((int)m).ToString(), Text = m.ToString() })
                .ToList();

            viewModel.AvailableFoods = _context.Foods
                .Where(f => f.FoodTypeId == viewModel.MenuTypeId)
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetAvailableFoods(int menuTypeId)
        {
            var availableFoods = _context.Foods
                .Where(f => f.FoodTypeId == (MenuTypeEnum)menuTypeId)
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.NameFood
                })
                .ToList();

            return Json(availableFoods);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //TODO eliminar este código cuando se haga la inyección de dependencia            
            var restaurant = await _context.Restaurants.FindAsync(id);
            //TODO reemplazar por:
            //TODO var restaurant = _restaurantService.GetById(id.Value);

            if (restaurant == null)
            {
                return NotFound();
            }

            var viewModel = new RestaurantEditViewModel
            {
                Restaurant = restaurant,
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
                AvailableFoods = _context.Foods
                   .Where(f => f.FoodTypeId == restaurant.MenuTypeId)
                   .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
                   .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantEditViewModel viewModel)
        {
            if (id != viewModel.Restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = await _context.Restaurants.Include(r => r.Foods).FirstOrDefaultAsync(r => r.Id == id);

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
                    //TODO eliminar este código cuando se haga la inyección de dependencia            
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                    //TODO reemplazar por:
                    //TODO _restaurantService.Update(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(viewModel.Restaurant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
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

            viewModel.AvailableFoods = await _context.Foods
                        .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.NameFood })
                        .ToListAsync();

            return View(viewModel);
        }

        //GET:Restaurant/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO eliminar este código cuando se haga la inyección de dependencia            
            var restaurant = await _context.Restaurants.FindAsync(id);
            //TODO reemplazar por:
            //TODO var restaurant = _restaurantService.GetById(id.Value);

            if (restaurant == null)
            {
                return NotFound();
            }

            try
            {
                // Eliminar las relaciones de FoodRestaurant
                //TODO verificar que esto no pinche cuando se elimina el restaurant, en el ejercicio se usa:
                //                  _restaurantService;
                var foodRestaurants = await _context.FoodRestaurants.Where(fr => fr.RestaurantId == id).ToListAsync();
                _context.FoodRestaurants.RemoveRange(foodRestaurants);

                // Eliminar el restaurante
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();

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
            //TODO eliminar este código cuando se haga la inyección de dependencia            
            var restaurant = await _context.Restaurants.FindAsync(id);
            //TODO reemplazar por:
            //TODO var restaurant = _restaurantService.GetById(id.Value);


            if (restaurant == null)
            {
                return NotFound();
            }

            //TODO verificar que esto no pinche cuando se elimina el restaurant, en el ejercicio se usa:
            //                  _restaurantService;
            // Eliminar las relaciones de FoodRestaurant del restaurante que estamos eliminando
            var foodRestaurants = _context.FoodRestaurants.Where(fr => fr.RestaurantId == id);
            _context.FoodRestaurants.RemoveRange(foodRestaurants);

            //TODO eliminar este código cuando se haga la inyección de dependencia    
            //                  _restaurantService;
            // Eliminar el restaurante
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            //TODO _restaurantService.Delete(restaurant);
            return RedirectToAction(nameof(Index));
        }
        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO eliminar este código cuando se haga la inyección de dependencia
            var restaurant = await _context.Restaurants
            .Include(r => r.Foods)
            .ThenInclude(fr => fr.Food)
            .FirstOrDefaultAsync(m => m.Id == id);
            //TODO reemplazar por:
            //TODO var restaurant = _restaurantService.GetById(id.Value);


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
