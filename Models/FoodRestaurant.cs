using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class FoodRestaurant
{
    public int Id;
    public int FoodId { get; set; }
    public int RestaurantId { get; set; }

}