using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Locations
    {
        public Locations()
        {
            Orders = new HashSet<Orders>();
            ProductInventory = new HashSet<ProductInventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
