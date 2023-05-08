using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stix.Utils;
namespace Stix.Models;

public class Restaurant
{
    
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nombre del Restaurant")]
    public string RestaurantName { get; set; }

    [Required]
    [Display(Name = "Calle")]
    public string Street { get; set; }

    [Required]
    [Display(Name = "Número")]
    public int Number { get; set; }

    [Display(Name = "Barrio")]
    public string Neighbourhood { get; set; }

    [Display(Name = "Localidad")]
    public string Town { get; set; }

    [Required]
    [Display(Name = "Provincia")]
    public string Provincia { get; set; }

    [Display(Name = "Menú del restaurant")]
    
    public virtual List<Food> Foods { get; set; }
}