using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class Category : IIorwoodEntity
    {
        public Category()
        {
            OrderList = 10;
        }
        public int Id { get; set; }
        [Required]
        [Display(Name="Category Name")]
        public string  Name { get; set; }
        [Display(Name="Order List")]
        public int OrderList { get; set; }
    }
}
