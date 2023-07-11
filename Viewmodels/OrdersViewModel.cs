using System.Collections;
using Stix.Models;
using Stix.Utils;
namespace Stix.ViewModels;


public class OrdersViewModel: IEnumerable<Order>
{
    public List<Order> Orders { get; set; } = new List<Order>();
    public string NameFilter { get; set; }
    public List<Food> AvailableFoods { get; set; }

     public IEnumerator<Order> GetEnumerator()
        {
            return Orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
}