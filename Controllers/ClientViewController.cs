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
using Stix.Services;
using Microsoft.AspNetCore.Authorization;

namespace Stix.Controllers
{
    public class ClientViewController : Controller
    {
        private readonly IFoodService _foodService;

        public ClientViewController(IFoodService foodservice)
        {
            _foodService = foodservice;
        }

        // GET: Food
        public async Task<IActionResult> Index(string NameFilter)
        {
            var model = new FoodViewModel();
            model.Foods = _foodService.GetAll(NameFilter);

            return View(model);
        }
    }
}