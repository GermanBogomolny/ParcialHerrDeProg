using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stix.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stix.ViewModels;

public class OrderDetailsViewModel
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
    public Order Order { get; set; }

}