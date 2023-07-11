using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stix.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stix.ViewModels;

public class RestaurantDetailsViewModel
{
    [Required]
    [Display(Name = "Nombre del Restaurant")]
    public string RestaurantName { get; set; }

    [Required]
    [Display(Name = "Calle")]
    public string Street { get; set; }

    [Required]
    [Display(Name = "Número")]
    public int Number { get; set; }

    [Display(Name = "Barrio")]
    public string Neighbourhood { get; set; }

    [Display(Name = "Localidad")]
    public string Town { get; set; }

    [Required]
    [Display(Name = "Provincia")]
    public string Provincia { get; set; }

    [Required]
    [Display(Name = "Menú del restaurant")]
    public MenuTypeEnum MenuTypeId { get; set; }

    public List<SelectListItem> MenuTypes { get; set; }

    public Restaurant Restaurant { get; set; }
    public List<SelectListItem> Foods { get; set; }

    public List<int> SelectedFoodIds { get; set; }
    public List<SelectListItem> AvailableFoods { get; set; }

    public List<SelectListItem> AllFoods { get; set; }

    public MenuTypeEnum MenuType => (MenuTypeEnum)MenuTypeId;

}

