using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly IorwoodDbContext _context;
        public StockRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
