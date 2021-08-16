using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Abstract
{
   public interface ICurrentMovementRepository:IRepository<CurrentMovement>
    {
        void setAccountingBookId(int AccountingBookId);

    }
}
