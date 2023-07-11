using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;

namespace Stix.ViewModels;
public class OrderEditViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Plato")]
    public string NameFood { get; set; }

    [Required]
    [Display(Name = "Descripción del plato")]
    public string DescriptionFood { get; set; }

    [Required]
    [Display(Name = "Precio")]
    public int Price { get; set; }

    [Display(Name = "Categoria")]
    public MenuTypeEnum FoodTypeId { get; set; }

    [Required]
    [Display(Name = "Cantidad")]
    public int Qantity { get; set; }

    [Required]
    [Display(Name = "Cliente")]
    public int ClientId { get; set; }

    public MenuTypeEnum MenuTypeId { get; set; }
    public List<SelectListItem>? MenuTypes { get; set; }

    public List<SelectListItem>? AvailableClients { get; set; }

    public List<SelectListItem>? AvailableFoods { get; set; }

}