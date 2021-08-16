using IorwoodDemo.Model.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Model.ViewModel
{
    public class OrderDetailsViewModel:IIorwoodViewModel
    {
        public OrderHeader OrderHeader{ get; set; }
        public List<OrderDetail> OrderDetailList { get; set; }
    }
}
