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


    public int FoodTypeId { get; set; }

    public virtual List<Restaurant> Restaurants { get; set; }
}
