using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Abstract
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void ChangeOrderStatus(int id, string status);
        public void ChangePaymentStatus(int id, string status);
    }
}
