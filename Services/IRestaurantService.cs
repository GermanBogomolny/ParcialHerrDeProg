using Stix.Models;
namespace Stix.Services;

public interface IRestaurantService
{
    void Create(Restaurant obj);
    List<Restaurant> GetAll(string filter);
    void Update(Restaurant obj);
    void Delete(Restaurant obj);
    Restaurant GetById(int id);
}
