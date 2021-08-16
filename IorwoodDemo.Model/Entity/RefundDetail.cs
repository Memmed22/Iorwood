using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
   public class RefundDetail : IIorwoodEntity
    {
        public int Id { get; set; }
        public int RefundHeaderId { get; set; }
        [ForeignKey("RefundHeaderId")]
        public virtual RefundHeader RefundHeader { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public double Price { get; set; }
        public int Count { get; set; }

    }
}
