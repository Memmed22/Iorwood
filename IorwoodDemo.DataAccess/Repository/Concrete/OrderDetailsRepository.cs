using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
     public class OrderDetailsRepository : Repository<OrderDetail>, IOrderDetailsRepository
    {
        private readonly IorwoodDbContext _context;
        public OrderDetailsRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
