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
    public class ExtraRepository :Repository<Extra>, IExtraRepository
    {
        private readonly IorwoodDbContext _context;
        public ExtraRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> ExtraListForDropDown()
        {
            return _context.Extra.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }) ;
        }


        public void Update(Extra extra)
        {
            var innerQue = _context.Extra.FirstOrDefault(u => u.Id == extra.Id);
            innerQue.Name = extra.Name;
            _context.SaveChanges();
        }
    }
}
