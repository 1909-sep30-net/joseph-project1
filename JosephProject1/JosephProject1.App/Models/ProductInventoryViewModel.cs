using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class ProductInventoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }


        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int MaxQuantity { get; set; }
    }
}
