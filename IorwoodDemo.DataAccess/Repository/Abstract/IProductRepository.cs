using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace IorwoodDemo.DataAccess.Repository.Abstract
{
   public interface IProductRepository : IRepository<Product>
    {
        
        void Activate(int id);

        void Deactivate(int id);
        void Update(Product product);

        void UpdateStockQuantity(int id, double quantity, double minQuantity);
        void AddStockQuantity(int id, double quantity);
        string MinusStockQuantity(int id, double quantity);
    }
}
