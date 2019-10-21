using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JosephProject1.App.Models
{
    public class CustomerInfoViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get => FirstName + " " + LastName; }
    }
}
