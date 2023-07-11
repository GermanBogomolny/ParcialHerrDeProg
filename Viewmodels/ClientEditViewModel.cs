using System.ComponentModel.DataAnnotations;

namespace Stix.ViewModels;


public class ClientEditViewModel
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
}