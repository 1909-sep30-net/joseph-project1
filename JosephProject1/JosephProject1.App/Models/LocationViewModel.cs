using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class LocationViewModel
    {
        [Display(Name = "Location Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Location Name")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Sales { get; set; }

       public IEnumerable<ProductInventoryViewModel> Inventory { get; set; }

       public IEnumerable<OrdersViewModel> Orders { get; set; }
    }
}
