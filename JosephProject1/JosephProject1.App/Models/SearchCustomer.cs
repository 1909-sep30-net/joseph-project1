using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class SearchCustomer
    {

        public int? Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        public string LastName { set; get; }

        public IEnumerable<CustomerInfoViewModel> customers { get; set; }
    }
}
