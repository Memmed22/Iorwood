using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Connections.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    public class RefundDetailRepository : Repository<RefundDetail>, IRefundDetailRepository
    {
        private readonly IorwoodDbContext _context;

        public RefundDetailRepository(IorwoodDbContext context): base(context)
        {
            _context = context;
        }
    }
}
