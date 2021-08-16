using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    public class CurrentMovementRepositroy : Repository<CurrentMovement>, ICurrentMovementRepository
    {
        private readonly IorwoodDbContext _context;

        public CurrentMovementRepositroy(IorwoodDbContext context):base(context)
        {
            _context = context;
        }

        public void setAccountingBookId(int AccountingBookId)
        {
            var unClearedCurrents = _context.CurrentMovement.Where(u => u.Cleared == false).ToList();//.Select(u => { u.AccountingBookId = AccountingBookId; u.Cleared = true; return true; });

            foreach (var item in unClearedCurrents)
            {
                item.Cleared = true;
                item.AccountingBookId = AccountingBookId;
                item.UpdateDate = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }
}
