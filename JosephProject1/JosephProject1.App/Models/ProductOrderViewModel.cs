using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class ProductOrderViewModel
    {
        [Display(Name = "Product Id")]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public int Quantity { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Total { get; set; }
    }
}
