using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stix.Data;
using Stix.Models;
using Stix.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Stix.Services;

namespace Stix.Controllers
{
    [Authorize(Roles = "AdminUsuarios, RestaurantManager")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderservice)
        {
            _orderService = orderservice;
        }

        // GET: Order
        public async Task<IActionResult> Index(string NameFilter)
        {
            var model = new OrdersViewModel();
            model.Orders = _orderService.GetAll(NameFilter);

            return View(model);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _orderService.GetById(id.Value);


            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                DescriptionFood = order.DescriptionFood,
                Price = order.Price,
                FoodTypeId = order.FoodTypeId,
                Qantity = order.Qantity,
                ClientId = order.ClientId,
            };


            return View(viewModel);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
             var viewModel = new OrderCreateViewModel
          {
             MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                            .Cast<MenuTypeEnum>()
                            .Select(e => new SelectListItem
                            {
                                Text = e.ToString(),
                                Value = ((int)e).ToString()
                            }).ToList(),
        AvailableFoods = _orderService.GetAvailableFoods(),
        AvailableClients = _orderService.GetAvailableClients()
    };

    return View(viewModel);
        }
        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel viewModel)
        {
            var model = new OrderCreateViewModel
            {
                MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                                  .Cast<MenuTypeEnum>()
                                  .Select(e => new SelectListItem
                                  {
                                      Text = e.ToString(),
                                      Value = ((int)e).ToString()
                                  }).ToList(),
                AvailableFoods = new List<SelectListItem>(),
                AvailableClients = new List<SelectListItem>()
            };
            viewModel.AvailableFoods = _orderService.GetAvailableFoods();
            viewModel.AvailableClients = _orderService.GetAvailableClients();
            

            ModelState.Remove("Client");
            ModelState.Remove("Foods");
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    NameFood = viewModel.NameFood,
                    DescriptionFood = viewModel.DescriptionFood,
                    Price = viewModel.Price,
                    FoodTypeId = viewModel.FoodTypeId,
                    Qantity = viewModel.Qantity,
                    ClientId = viewModel.ClientId,
                };
                _orderService.Create(order, viewModel);
                return RedirectToAction(nameof(Index));
            }

            viewModel.MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                .Cast<MenuTypeEnum>()
                .Select(m => new SelectListItem { Value = ((int)m).ToString(), Text = m.ToString() })
                .ToList();
            viewModel.AvailableClients = _orderService.GetAvailableClients();
            //viewModel = _orderService.Listar(viewModel);

            return View(viewModel);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var order = _orderService.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderEditViewModel
            {
                NameFood = order.NameFood,
                DescriptionFood = order.DescriptionFood,
                Price = order.Price,
                FoodTypeId = order.FoodTypeId,
                Qantity = order.Qantity,
                ClientId = order.ClientId,
                MenuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                               .Cast<MenuTypeEnum>()
                               .Select(e => new SelectListItem
                               {
                                   Text = e.ToString(),
                                   Value = ((int)e).ToString()
                               }).ToList(),
                AvailableFoods = new List<SelectListItem>(),
                AvailableClients = new List<SelectListItem>()
            };

            return View(viewModel);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderEditViewModel viewModel)
        {

            ModelState.Remove("Client");
            ModelState.Remove("MenuTypes");
            ModelState.Remove("Foods");
            if (ModelState.IsValid)
            {
                var order = _orderService.GetById(id);
                if (order == null)
                {
                    return NotFound();
                }
                order.NameFood = viewModel.NameFood;
                order.DescriptionFood = viewModel.DescriptionFood;
                order.Price = viewModel.Price;
                order.FoodTypeId = viewModel.FoodTypeId;
                order.Qantity = viewModel.Qantity;
                order.ClientId = viewModel.ClientId;

                _orderService.Update(order);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
