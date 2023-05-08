using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class FoodType
{
    public int FoodId;
    public int RestaurantId;

    public List<Restaurant> Restaurant;
    public List<Food> Food;

}