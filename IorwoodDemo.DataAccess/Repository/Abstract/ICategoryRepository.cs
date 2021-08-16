using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace IorwoodDemo.DataAccess.Repository.Abstract
{
   public interface ICategoryRepository : IRepository<Category>
    {
        public IEnumerable<SelectListItem> CategoryListForDropDown();

        void Update(Category category);
    }
}
