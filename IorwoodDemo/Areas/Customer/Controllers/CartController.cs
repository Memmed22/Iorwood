using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Extensions;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Model.ViewModel;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IorwoodDemo.Areas.Customer.Controllers
{
   
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public List<ProductDetailsViewModel> productListVM { get; set; }
        public OrderDetailsViewModel OrderViewModel { get; set; }

        public List<object> SessionCartList { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
       
        }



        public IActionResult Index()
        {
            SessionCartList = HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart); //Sessiondan verileri cektik.
            List<object> SessionProducts = new List<object>();
            productListVM = new List<ProductDetailsViewModel>();


            if (SessionCartList != null)
                foreach (var item in SessionCartList)
                {

                    SessionProducts = ((IEnumerable<object>)item).ToList();
                    //Optimize edilmesi lazim, butu colonlar lazim deyil

                    var innerProduct = _unitOfWork.Product.FirstOrDefault(u => u.Id == Convert.ToInt32(SessionProducts[0]));


                    productListVM.Add(new ProductDetailsViewModel()
                    {
                        Count = Convert.ToInt32(SessionProducts[1]),
                        ProductName = innerProduct.Name,
                        Price = innerProduct.Price,
                        Image = innerProduct.Image,
                        ProductId = innerProduct.Id,
                        
                    });
                }


            return View(productListVM);
        }


        [HttpPost]
        public IActionResult Summary()
        {
            OrderDetailsViewModel OrderViewModel = new OrderDetailsViewModel()
            {
                OrderHeader = new OrderHeader(),
                OrderDetailList = new List<OrderDetail>()
            };
            SessionCartList = HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart); //Sessiondan verileri cektik.
            List<object> SessionProducts = new List<object>();

            if (SessionCartList.Count == 0)
                return RedirectToAction(nameof(Index));  //Home yonlendirmek lazimdi
            double totalAmout = 0;
            foreach (var item in SessionCartList)
            {
                SessionProducts = ((IEnumerable<object>)item).ToList();

                Product innerProdcut = _unitOfWork.Product.FirstOrDefault(u => u.Id == Convert.ToInt32(SessionProducts[0]));
                totalAmout += (innerProdcut.Price * Convert.ToInt32(SessionProducts[1]));

                OrderViewModel.OrderDetailList.Add(new OrderDetail() { 
                    Count = Convert.ToInt32(SessionProducts[1]),
                    Price = innerProdcut.Price,
                    ProductName = innerProdcut.Name,
                    ProductId =  innerProdcut.Id
                     
                });
            }

            OrderViewModel.OrderHeader.TotalAmount = totalAmout;

            return View(OrderViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult OrderSubmit(OrderDetailsViewModel OrderViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Summary));
             OrderViewModel.OrderHeader.OrderDate = DateTime.Now;
            OrderViewModel.OrderHeader.PaymentStatus = StaticValue.PaymentIsWaiting;
            OrderViewModel.OrderHeader.PickUpDateTime = new DateTime(OrderViewModel.OrderHeader.PickUpDate.Year, OrderViewModel.OrderHeader.PickUpDate.Month, OrderViewModel.OrderHeader.PickUpDate.Day,
                OrderViewModel.OrderHeader.PickupTime.Hour, OrderViewModel.OrderHeader.PickupTime.Minute, OrderViewModel.OrderHeader.PickupTime.Second);
            OrderViewModel.OrderHeader.Status = StaticValue.OrderSubmitted;
            

            _unitOfWork.OrderHeader.Add(OrderViewModel.OrderHeader);

            _unitOfWork.Save();
           
            foreach (var item in OrderViewModel.OrderDetailList)
            { 
                item.OrderHeaderId = OrderViewModel.OrderHeader.Id;
               
                _unitOfWork.OrderDetails.Add(item);
            }

            _unitOfWork.Save();
            HttpContext.Session.SetObject(StaticValue.ShoppingCart, null);
            return View(OrderViewModel.OrderHeader);
        }

        #region API CALLS

        [HttpPost]
        public IActionResult Increament(int id)
        {
            SessionCartList = HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart);
            List<object> SessionProducts = new List<object>();
            if (SessionCartList == null)
                return Json(new { success = false });

            foreach (var item in SessionCartList)
            {
                SessionProducts = ((IEnumerable<object>)item).ToList();
                if (Convert.ToInt32(SessionProducts[0]) == id)
                {
                    int elIndex = SessionCartList.IndexOf(item);
                    SessionCartList.Remove(item);
                    SessionProducts[1] = Convert.ToInt32(SessionProducts[1]) + 1;
                    SessionCartList.Insert(elIndex, SessionProducts);

                    break;
                }
            }

            HttpContext.Session.SetObject(StaticValue.ShoppingCart, SessionCartList);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Decreament(int id)
        {

            SessionCartList = HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart);
            List<object> SessionProducts = new List<object>();
            if (SessionCartList == null)
                return Json(new { success = false, message = "Somthing wrong, please try later again.." });

            foreach (var item in SessionCartList)
            {
                SessionProducts = ((IEnumerable<object>)item).ToList();
                if (Convert.ToInt32(SessionProducts[0]) == id)
                {
                    if (Convert.ToInt32(SessionProducts[1]) != 1)
                    {
                        int elIndex = SessionCartList.IndexOf(item);
                        SessionCartList.Remove(item);
                        SessionProducts[1] = Convert.ToInt32(SessionProducts[1]) - 1;
                        SessionCartList.Insert(elIndex, SessionProducts);
                    }
                    else
                    {
                        return Json(new { success = false, message = "EmptySession" });
                    }
                    break;
                }
            }

            HttpContext.Session.SetObject(StaticValue.ShoppingCart, SessionCartList);
            return Json(new { success = true });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SessionCartList = HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart);
            List<object> SessionProducts = new List<object>();
            if (SessionCartList == null)
            {
                return Json(new { success = false, message = "Somthing wrong, please try later again.." });
            }
            foreach (var item in SessionCartList)
            {
                SessionProducts = ((IEnumerable<object>)item).ToList();
                if (Convert.ToInt32(SessionProducts[0]) == id)
                {
                    SessionCartList.Remove(item);
                    break;
                }
            }
            HttpContext.Session.SetObject(StaticValue.ShoppingCart, SessionCartList);
            return Json(new { success = true });

        }

        #endregion
    }
}