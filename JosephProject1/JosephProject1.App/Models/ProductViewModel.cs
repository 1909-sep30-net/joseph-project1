﻿using System.ComponentModel.DataAnnotations;

namespace JosephProject1.App.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
    }
}