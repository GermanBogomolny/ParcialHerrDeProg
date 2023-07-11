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
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientservice)
        {
            _clientService = clientservice;
        }

        // GET: Client
        public async Task<IActionResult> Index(string NameFilter)
        {
            var model = new ClientViewModel();
            model.Clients = _clientService.GetAll(NameFilter);

            return View(model);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client, ClientEditViewModel viewModel)
        {
            ModelState.Remove("Orders");
            if (ModelState.IsValid)
            {
                _clientService.Create(client, viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = _clientService.GetById(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            var model = new ClientEditViewModel
            {
                NameClient = client.NameClient,
                SurnameClient = client.SurnameClient,
                PhoneClient = client.PhoneClient,
                EmailClient = client.EmailClient
            };
            return View(model);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameClient,SurnameClient,PhoneClient,EmailClient")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            _clientService.Update(client);

            return RedirectToAction(nameof(Index));
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _clientService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
