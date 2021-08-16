using IorwoodDemo.Model.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Model.ViewModel
{
    public class HomePageViewModel: IIorwoodViewModel
    {
        public List<Category> CategroyList { get; set; }
        public List<Product> ProductList { get; set; }

        public List<HomePageViewModel2> HomePageList { get; set; }
    }

    public class HomePageViewModel2 : IIorwoodViewModel
    {
        public Category Category { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
