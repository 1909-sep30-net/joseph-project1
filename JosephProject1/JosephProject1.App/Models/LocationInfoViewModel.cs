using System.Collections.Generic;

namespace JosephProject1.App.Models
{
    public class LocationInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ProductInventoryViewModel> Inventory { get; set; }
    }
}
