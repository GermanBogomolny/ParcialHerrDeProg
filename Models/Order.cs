namespace Stix.Models;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Plato")]
    public string NameFood { get; set; }

    [Required]
    [Display(Name = "Descripción del plato")]
    public string DescriptionFood { get; set; }

    [Required]
    [Display(Name = "Precio")]
    public int Price { get; set; }

    [Display(Name = "Categoria")]
    public MenuTypeEnum FoodTypeId { get; set; }

    [Required]
    [Display(Name = "Cantidad")]
    public int Qantity { get; set; }

    public Client Client { get; set; }
    public virtual List<Food> Foods { get; set; }
}
