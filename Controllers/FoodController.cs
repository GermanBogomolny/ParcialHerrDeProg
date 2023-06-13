using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Viewmodels;
using Stix.Services;
using Microsoft.AspNetCore.Authorization;

namespace Stix.Controllers

{
    [Authorize(Roles = "AdminUsuarios, RestaurantManager")]

    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodservice)
        {
            _foodService = foodservice;
        }


        // GET: Food
        [AllowAnonymous]
        public async Task<IActionResult> Index(string NameFilter)
        {

            var model = new FoodViewModel();
            model.Foods = _foodService.GetAll(NameFilter);

            return View(model);
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = _foodService.GetById(id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food, FoodCreateViewModel viewModel)
        {
            ModelState.Remove("IsVegetarianFood");
            ModelState.Remove("IsVeganFood");
            ModelState.Remove("Restaurants");
            if (ModelState.IsValid)
            {
                _foodService.Create(food, viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = _foodService.GetById(id);

            return View(food);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameFood,DescriptionFood,IsVeganFood,IsVegetarianFood,Price,FoodTypeId")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            try
            {
                _foodService.Update(food);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(food.Id))
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

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = _foodService.GetById(id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _foodService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _foodService.FoodExists(id);
        }
    }
}
