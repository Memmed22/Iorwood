using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly IorwoodDbContext _context;
        public OrderHeaderRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeOrderStatus(int id, string status)
        { 
            var order = _context.OrderHeader.Find(id);
            order.Status = status;
            _context.SaveChanges();

        }

        public void ChangePaymentStatus(int id, string status)
        {
            var order = _context.OrderHeader.Find(id);
            order.PaymentStatus = status;
            _context.SaveChanges();
        }

    }
}
