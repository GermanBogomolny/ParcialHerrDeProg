using System.ComponentModel.DataAnnotations;
using Stix.Utils;

namespace Stix.Models;

public class FoodType
{
    public int FoodsId;
    public int RestaurantsId;

    public List<Restaurant> Restaurants;
    public List<Food> Foods;

}