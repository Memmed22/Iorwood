using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class OrderDetail : IIorwoodEntity
    {
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }

    }
}
