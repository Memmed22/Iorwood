using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
   public class RefundHeaderRepository : Repository<RefundHeader>, IRefundHeaderRepository
    {
        private readonly IorwoodDbContext _context;

        public RefundHeaderRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
