using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class ProductInventory
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Products Product { get; set; }
    }
}
