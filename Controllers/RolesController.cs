using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stix.Models;
using Microsoft.AspNetCore.Identity;
using Stix.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Stix.Controllers;

    [Authorize(Roles = "AdminUsuarios")]
public class RolesController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    public RolesController (
        ILogger<HomeController> logger,
        RoleManager<IdentityRole> roleManager){
            _logger = logger;
            _roleManager = roleManager;
        }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create (RolesCreateViewmodel model)
    {
        if(string.IsNullOrEmpty(model.RoleName))
        {
            return View();
        }

        var role = new IdentityRole(model.RoleName);
        _roleManager.CreateAsync(role);

        return RedirectToAction("Index");
    }
}
