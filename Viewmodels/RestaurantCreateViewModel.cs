using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.Utils;

namespace Stix.ViewModels
{
    public class RestaurantCreateViewModel
    {
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

        [Required]
        [Display(Name = "Menú del restaurant")]
        public MenuTypeEnum MenuTypeId { get; set; }

        public virtual List<SelectListItem>? MenuTypes { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual List<SelectListItem>? Foods { get; set; }

        public List<int> SelectedFoodIds { get; set; }
        public virtual List<SelectListItem>? AvailableFoods { get; set; }
        
    }
}
