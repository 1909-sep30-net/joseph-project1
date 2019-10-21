using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JosephProject1.App.Models
{
    public class LocationInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ProductInventoryViewModel> Inventory { get; set; }
    }
}
