using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Products
    {
        public Products()
        {
            ProductInventory = new HashSet<ProductInventory>();
            ProductOrdered = new HashSet<ProductOrdered>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
        public virtual ICollection<ProductOrdered> ProductOrdered { get; set; }
    }
}
