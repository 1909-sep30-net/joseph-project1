using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JosephProject1.App.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalPuchases { get; set; }
    }
}
