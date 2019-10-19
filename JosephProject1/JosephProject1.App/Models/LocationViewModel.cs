using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Sales { get; set; }

       public IEnumerable<InventoryViewModel> Inventory { get; set; }
       public IEnumerable<OrdersViewModel> Orders { get; set; }
    }
}
