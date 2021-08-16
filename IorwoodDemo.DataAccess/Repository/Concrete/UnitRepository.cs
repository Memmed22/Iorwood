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
    internal class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly IorwoodDbContext _context;
        public UnitRepository(IorwoodDbContext context) :base(context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> UnitListForDropDown()
        {
            return _context.Unit.Select(u => new SelectListItem()
            {
                Text = u.Number + " - " + u.Name,
                Value = u.Id.ToString()
            }).ToList();
        }

        public void Update(Unit unit)
        {
            var item = _context.Unit.FirstOrDefault(u => u.Id == unit.Id);
            item.Name = unit.Name;
            item.Number = unit.Number;
            _context.SaveChanges();
        }
    }
}
