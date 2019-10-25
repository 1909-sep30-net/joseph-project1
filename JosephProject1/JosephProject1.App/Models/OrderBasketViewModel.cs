using System.Collections.Generic;

namespace JosephProject1.App.Models
{
    public class OrderBasketViewModel
    {
        public int LocationId { get; set; }
        public int CustomerId { get; set; }

        public List<CustomerInfoViewModel> CustomersInfo { get; set; } = new List<CustomerInfoViewModel>();
        public List<ProductInventoryViewModel> OrderInfo { get; set; } = new List<ProductInventoryViewModel>();
    }
}
