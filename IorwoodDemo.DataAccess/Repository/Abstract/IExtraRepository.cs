using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Abstract
{
    public interface IExtraRepository : IRepository<Extra>
    {
        public IEnumerable<SelectListItem> ExtraListForDropDown();
        void Update(Extra extra);
    }
}
