using System.ComponentModel.DataAnnotations;
namespace Stix.Utils;

public enum FoodId
{
    [Display(Name = "Sushi")]
    Sushi,
    [Display(Name = "Vegano")]
    Vegan,
    [Display(Name = "Vegetariano")]
    Vegetarian

}
