using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class Food
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Plato")]
    public string NameFood { get; set; }

    [Required]
    [Display(Name = "Descripción del plato")]
    public string DescriptionFood { get; set; }

    [Display(Name = "Vegano")]
    public bool IsVeganFood { get; set; }

    [Display(Name = "Vegetariano")]
    public bool IsVegetarianFood { get; set; }

    [Required]
    [Display(Name = "Precio")]
    public int Price { get; set; }

    [Display(Name = "Categoria")]
    public MenuTypeEnum FoodTypeId { get; set; }

    public virtual List<FoodRestaurant> Restaurants { get; set; }
}
