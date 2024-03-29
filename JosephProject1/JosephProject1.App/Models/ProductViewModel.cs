﻿using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class ProductViewModel
    {
        [Display(Name = "Product Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Range(0.0, 50000)]
        [Required(ErrorMessage = "Product Price is required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
    }
}
