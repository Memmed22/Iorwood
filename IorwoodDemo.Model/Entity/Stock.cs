using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
   public class Stock : IIorwoodEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CareateDateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public int MinQuantity { get; set; }
    }
}
