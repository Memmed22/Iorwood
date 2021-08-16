using IorwoodDemo.DataAccess.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.UnitOfWork.Abstract
{
   public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository Category { get; }
        public IExtraRepository Extra { get; }
        public IUnitRepository Unit { get; }
        public IProductRepository Product { get; }
        public IOrderHeaderRepository OrderHeader { get;}
        public IOrderDetailsRepository OrderDetails { get;}
        public IStockRepository StockRepository { get; }
        public IRefundHeaderRepository RefundHeader { get; }
        public IRefundDetailRepository RefundDetail { get; }
        public ICurrentMovementRepository CurrentMovement { get; }
        public IAccountingBookRepository AccountingBook { get; }
        public IUserApplicationRepository UserApplication { get; }
        void Save();
    }
}
