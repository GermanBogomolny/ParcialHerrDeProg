namespace Stix.Models;
using System.ComponentModel.DataAnnotations;

public class Client
{
    [Display(Name = "Número de cliente")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nombre del cliente")]
    public string NameClient { get; set; }

    [Required]
    [Display(Name = "Apellido del cliente")]
    public string SurnameClient { get; set; }

    [Required]
    [Display(Name = "Teléfono")]
    public int PhoneClient { get; set; }

    [Required]
    [Display(Name = "Email")]
    public string EmailClient { get; set; }

    public virtual List<Order> Orders {get;set;}

}
