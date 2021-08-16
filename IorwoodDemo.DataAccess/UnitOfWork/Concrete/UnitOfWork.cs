using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.DataAccess.Repository.Concrete;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IorwoodDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IExtraRepository Extra { get; private set; }
        public IUnitRepository Unit { get; private set; }
        public IProductRepository Product { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IStockRepository StockRepository { get; private set; }
        public IRefundHeaderRepository RefundHeader { get; private set; }

        public IRefundDetailRepository RefundDetail { get; private set; }
        public ICurrentMovementRepository CurrentMovement { get; private set; }

        public IAccountingBookRepository AccountingBook { get; private set; }

        public IUserApplicationRepository UserApplication { get; set; }

        public UnitOfWork(IorwoodDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Extra = new ExtraRepository(_context);
            Unit = new UnitRepository(_context);
            Product = new ProductRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetails = new OrderDetailsRepository(_context);
            StockRepository = new StockRepository(_context);
            RefundHeader = new RefundHeaderRepository(_context);
            RefundDetail = new RefundDetailRepository(_context);
            CurrentMovement = new CurrentMovementRepositroy(_context);
            AccountingBook = new AccountingBookRepository(_context);
            UserApplication = new UserApplicationRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
