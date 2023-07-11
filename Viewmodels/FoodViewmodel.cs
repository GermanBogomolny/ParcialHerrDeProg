using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.Utils;

namespace Stix.ViewModels;


public class FoodViewModel
{
    public List<Food> Foods { get; set; } = new List<Food>();
    public string NameFilter { get; set; }
}