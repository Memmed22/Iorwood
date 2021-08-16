using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using IorwoodDemo.Extensions;
using IorwoodDemo.Model.ViewModel;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public CartHelper CartHelper { get; set; }
        HomePageViewModel HomePageVM;
        [BindProperty]
        public ProductDetailsViewModel ShoppingCartVM { get; set; }
        public HomeController(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            HomePageVM = new HomePageViewModel();
            ShoppingCartVM = new ProductDetailsViewModel();
            _accessor = accessor;
            CartHelper = new CartHelper(_accessor);
        }
       
        public IActionResult Index()
        {
            
            if(HttpContext.User.Identity == null || HttpContext.User.Identity.Name == null)
                return LocalRedirect("/Identity/Account/Login");

            var user = HttpContext.User.IsInRole("Manager");


         var k =   _accessor.HttpContext.User;
            HomePageVM = new HomePageViewModel()
            {
              
                CategroyList = _unitOfWork.Category.GetAll().ToList(),
                ProductList = _unitOfWork.Product.GetAll(includeProperties: "Extra,Unit", filter:u=> u.Active==true).ToList()
            };
            return View(HomePageVM);
        }

        public IActionResult AddToCart()
        {
            CartHelper.AddToCart(ShoppingCartVM.ProductId, ShoppingCartVM.Count);

            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Details(int id)
        {
           
            var product = _unitOfWork.Product.FirstOrDefault(u => u.Id == id, includeProperties : "Category,Unit");
            ShoppingCartVM = new ProductDetailsViewModel()
            {
                Category = product.Category.Name,
                Count = 1,
                Description = product.ShortDescription,
                // Extra = product.Extra.Name,
                Price = product.Price,
                ProductId = product.Id,
                ProductName = product.Name,
                Unit = product.Unit.NumberWithName,
                Image = product.Image,
                IsInCart = CartHelper.IsInCart(id)
            };

            return View(ShoppingCartVM);
        }
    }
}