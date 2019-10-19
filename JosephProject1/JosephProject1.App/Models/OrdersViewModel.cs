using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class OrdersViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Location Id")]
        public int LocationId { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Total { get; set; }

        public IEnumerable<ProductOrderViewModel> ProductOrders { get; set; }
    }
}
