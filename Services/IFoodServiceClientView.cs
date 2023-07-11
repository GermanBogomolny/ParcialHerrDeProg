using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.ViewModels;

namespace Stix.Services;

public interface IFoodServiceClientView
{
    List<Food> GetAll(string filter);
}
