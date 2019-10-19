using System;
using System.Collections.Generic;

namespace Project1.DataAccess.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            ProductOrdered = new HashSet<ProductOrdered>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Locations Location { get; set; }
        public virtual ICollection<ProductOrdered> ProductOrdered { get; set; }
    }
}
