using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName { get => FirstName + " " + LastName; }

        [Display(Name = "Total Purchases")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalPurchases { get; set; }

        public IEnumerable<OrdersViewModel> Orders { get; set; }


    }
}
