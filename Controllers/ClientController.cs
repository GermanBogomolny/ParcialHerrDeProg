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

    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientservice)
        {
            _clientService = clientservice;
        }

        // GET: Clients

        //TODO: Cambiar esta consulta por una consulta igual pero de clientes y NO de comidas
        public async Task<IActionResult> Index(string NameFilter)
        {
            var model = new FoodViewModel();
            //model.Foods = _clientService.GetAll(NameFilter);

            return View(model);
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = _clientService.GetById(id);
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
/*        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client, ClientViewModel viewModel)
        {
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
        */
    }
}
