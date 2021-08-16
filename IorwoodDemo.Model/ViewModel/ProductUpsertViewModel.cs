using IorwoodDemo.Model.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Model.ViewModel
{
  public  class ProductUpsertViewModel : IIorwoodViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> UnitList { get; set; }
        public List<SelectListItem> ExtraList { get; set; }

    }
}
