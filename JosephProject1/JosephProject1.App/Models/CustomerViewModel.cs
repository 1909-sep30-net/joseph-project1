using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Total Puchases")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalPurchases { get; set; }

        public IEnumerable<OrdersViewModel> Orders { get; set; }


    }
}
