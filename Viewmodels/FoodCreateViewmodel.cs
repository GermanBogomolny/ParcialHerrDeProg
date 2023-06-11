using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class FoodCreateViewModel
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
}