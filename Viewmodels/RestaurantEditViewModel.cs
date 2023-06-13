using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stix.Models;
using Stix.Utils;

namespace Stix.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required]
        [Display(Name = "Nombre del Restaurant")]
        public virtual string? RestaurantName { get; set; }

        [Required]
        [Display(Name = "Calle")]
        public virtual string? Street { get; set; }

        [Required]
        [Display(Name = "Número")]
        public virtual int Number { get; set; }

        [Display(Name = "Barrio")]
        public virtual string? Neighbourhood { get; set; }

        [Display(Name = "Localidad")]
        public virtual string? Town { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public virtual string? Provincia { get; set; }

        [Required]
        [Display(Name = "Menú del restaurant")]
        public virtual MenuTypeEnum MenuTypeId { get; set; }

        public virtual List<SelectListItem>? MenuTypes { get; set; }

        public virtual Restaurant? Restaurant { get; set; }

        public List<int>? SelectedFoodIds { get; set; }
        public virtual List<SelectListItem>? AvailableFoods { get; set; }
        
    }
}
