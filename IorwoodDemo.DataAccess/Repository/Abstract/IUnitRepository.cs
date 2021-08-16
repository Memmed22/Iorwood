using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace IorwoodDemo.DataAccess.Repository.Abstract
{
   public interface IUnitRepository : IRepository<Unit>
    {
        public IEnumerable<SelectListItem> UnitListForDropDown();

        void Update(Unit unit);
    }
}
