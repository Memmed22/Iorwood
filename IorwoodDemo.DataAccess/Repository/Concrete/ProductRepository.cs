using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IorwoodDbContext _context;
        public ProductRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }


        public void Activate(int id)
        {
            var innerProduct = _context.Product.FirstOrDefault(u => u.Id == id);
            innerProduct.Active = true;
            _context.SaveChanges();
            
        }

        public void AddStockQuantity(int id, double quantity)
        {
            var innerProduct = _context.Product.FirstOrDefault(u => u.Id == id);
            innerProduct.StockQuantity = Math.Round((innerProduct.StockQuantity + quantity), 4);
            _context.SaveChanges();
        }

        public void Deactivate(int id)
        {
            var innerProduct = _context.Product.FirstOrDefault(u => u.Id == id);
            innerProduct.Active = false;
            _context.SaveChanges();
        }

        public string MinusStockQuantity(int id, double quantity)
        {
            var innerProduct = _context.Product.FirstOrDefault(u => u.Id == id);
            innerProduct.StockQuantity =Math.Round((innerProduct.StockQuantity - quantity), 4);
            _context.SaveChanges();
            if (innerProduct.StockQuantity <= innerProduct.StockMinQuantity)
                return StaticValue.StockLessThanMinimum;
            return null;
        }

        public void Update(Product product)
        {
            var innderProduct = _context.Product.FirstOrDefault(u => u.Id == product.Id);
            innderProduct.CategoryId = product.CategoryId;
            innderProduct.ExtraId = product.ExtraId;
            innderProduct.ExtraInfo1 = product.ExtraInfo1;
            innderProduct.ExtraInfo2 = product.ExtraInfo2;
            innderProduct.Image = product.Image;
            innderProduct.Name = product.Name;
            innderProduct.Price = product.Price;
            innderProduct.UnitId = product.UnitId;

            _context.SaveChanges();

        }

        public void UpdateStockQuantity(int id, double quantity, double minQuantity)
        {
            var innderProduct = _context.Product.FirstOrDefault(u => u.Id == id);
            innderProduct.StockMinQuantity = Math.Round(minQuantity, 4);
            innderProduct.StockQuantity = Math.Round(quantity,4 );
            _context.SaveChanges();
        }
    }
}
