using Stix.Models;

namespace Stix.ViewModels;
public class ClientViewModel
{
    public List<Client> Clients { get; set; } = new List<Client>();
    public string NameFilter { get; set; }
   
}