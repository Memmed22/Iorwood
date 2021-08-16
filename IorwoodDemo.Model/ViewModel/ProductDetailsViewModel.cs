using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IorwoodDemo.Model.ViewModel
{
   public class ProductDetailsViewModel : IIorwoodViewModel
    {
        public ProductDetailsViewModel()
        {
            Count = 1;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string  Category { get; set; }
        public string Unit { get; set; }
        public string  Extra { get; set; }
        public string Description { get; set; }
        [Required]
        public int Count { get; set; }
        public double Price { get; set; }
        public string  Image { get; set; }
        public bool IsInCart { get; set; }
    }
}
