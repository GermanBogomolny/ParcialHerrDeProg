using System.ComponentModel.DataAnnotations;
namespace Stix.Utils;

public enum FoodTypeEnum
{
    [Display(Name = "Sushi")]
    Sushi,
    [Display(Name = "Vegano")]
    Vegan,
    [Display(Name = "Vegetariano")]
    Vegetarian
}