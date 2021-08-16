using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class RefundHeader:IIorwoodEntity
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }
        public double TotalAmount { get; set; }
        public DateTime RefundDate { get; set; }

        public string Comment { get; set; }

    }
}
