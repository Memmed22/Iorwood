using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class Product : IIorwoodEntity
    {
        public Product()
        {
            Active = true;
            StockQuantity = 0;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string ShortDescription { get; set; }
        [Display(Name ="Extra Info 1")]
        public string ExtraInfo1 { get; set; }
        [Display(Name = "Extra Info 2")]
        public string ExtraInfo2 { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 10000, ErrorMessage ="Price must be bigger than 0")]
        public double Price { get; set; }
        
        
        public string Image { get; set; }

        [Display(Name = "Category Type")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
       
        public virtual Category Category { get; set; }

        [Display(Name = "Unit Type")]
        [Required]
        public int UnitId  { get; set; }

        [ForeignKey("UnitId")]
       
        public virtual Unit Unit { get; set; }

        [Display(Name = "Extra")]
       
        public int? ExtraId { get; set; }

        [ForeignKey("ExtraId")]
        public virtual Extra Extra { get; set; }

        public bool Active { get; set; }

        [Display(Name="Stock Quantity")]
        public double StockQuantity { get; set; }

        [Display(Name = "Minimum Quantity")]
        public double StockMinQuantity { get; set; }


    }
}
