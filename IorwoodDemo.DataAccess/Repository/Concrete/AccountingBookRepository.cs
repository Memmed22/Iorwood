using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
   public class AccountingBookRepository : Repository<AccountingBook>, IAccountingBookRepository
    {
        private IorwoodDbContext _context;
        public AccountingBookRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }
    } 
}
