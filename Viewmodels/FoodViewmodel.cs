using Stix.Models;
namespace Viewmodels;

public class FoodViewmodel
{
    public List<Food> Foods { get; set; } = new List<Food>();
    public string NameFilter { get; set; }
}