using Stix.Models;
using Stix.Utils;
namespace Viewmodels;

public class RestaurantViewmodel
{
    public List<Food> Foods { get; set; } = new List<Food>();
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public string NameFilter { get; set; }
}