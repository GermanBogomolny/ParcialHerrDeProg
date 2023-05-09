using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class FoodRestaurant
{
    public int FoodId { get; set; }
    public int RestaurantId { get; set; }

    public virtual Food Food { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
