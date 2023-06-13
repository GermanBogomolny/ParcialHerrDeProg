using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.ViewModels;
using Viewmodels;

namespace Stix.Services;

public interface IClientService
{
    void Create(Client client);
    List<Client> GetAll(string filter);
    void Update(Client obj);
    void Delete(int id);
    Client? GetById(int? id);
    bool ClientExists(int id);

}