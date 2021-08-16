using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IorwoodDbContext _context;
        public CategoryRepository(IorwoodDbContext context) :base(context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> CategoryListForDropDown()
        {
            return _context.Category.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();

        }

        public void Update(Category category)
        {
            var item = _context.Category.FirstOrDefault(u => u.Id == category.Id);
            item.Name = category.Name;
            item.OrderList = category.OrderList;
            _context.SaveChanges();
        }
    }
}
