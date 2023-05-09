using Stix.Models;
namespace Viewmodels;

public class FoodViewModel
{
    public List<Food> Foods { get; set; } = new List<Food>();
    public string NameFilter { get; set; }
}