  using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Model.ViewModel;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailsViewModel> OrderDetailsVM { get; set; }
        
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderManagement()
        {
            List <OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == StaticValue.OrderOnTheWay || 
            u.Status == StaticValue.OrderSubmitted).ToList();
            OrderDetailsVM = new List<OrderDetailsViewModel>();

            if (orderHeaders != null)
            foreach (var item in orderHeaders)
            {
                    OrderDetailsVM.Add(new OrderDetailsViewModel()
                    {
                        OrderHeader = item,
                        OrderDetailList = _unitOfWork.OrderDetails.GetAll(u => u.OrderHeaderId == item.Id).ToList()
                    });
            }


            return View(OrderDetailsVM);
        }

        public IActionResult Details(int id)
        {
            OrderDetailsViewModel OrderDetailsVM = new OrderDetailsViewModel() 
            { OrderHeader = _unitOfWork.OrderHeader.FirstOrDefault(u=> u.Id == id), OrderDetailList = _unitOfWork.OrderDetails.GetAll(filter:u=> u.OrderHeaderId == id).ToList()};

            return View(OrderDetailsVM);
        }

        public IActionResult ProdcutList()
        {
            return View();
        }


        #region LocalSaleOperations

        public IActionResult LocalSale() => View();

        public IActionResult LocalSaleSummary()
        {
            return View();
        }


        [HttpPost]
        public IActionResult LocalSaleSubmitted(string postOrderString, string[] postOrderDetailList)
        {
            var userName = User.Identity.Name;
            var claimIntetity = (ClaimsIdentity)User.Identity;
            var claim = claimIntetity.FindFirst(ClaimTypes.NameIdentifier);
            
            
            if(postOrderString == null)
                return Json(new { success = false, message = "empty content" });

            int headerId = -1;
            try
            {
  string[] orderHeadeArray = postOrderString.Split(new char[] { ',' });
            string FullName = orderHeadeArray[0];
            string Adress = orderHeadeArray[1];
            string TelNo = orderHeadeArray[2];
            string Comment = orderHeadeArray[3];
            double TotalAmount = Convert.ToDouble(orderHeadeArray[4].Trim());
            OrderHeader header = new OrderHeader()
            {
                Adress = Adress,
                Comment = Comment,
                FullName = FullName,
                OrderDate = DateTime.Now,
                PaymentStatus = StaticValue.LocalSaleProcess, 
                PhoneNumber = TelNo,
                PickUpDate = DateTime.Now,
                Status = StaticValue.LocalSaleProcess,
                TotalAmount = TotalAmount
            };

            _unitOfWork.OrderHeader.Add(header);

                

                CurrentMovement currentMovement = new CurrentMovement()
                {
                    //AccountingBookId
                    CurrentDate = DateTime.Now,
                    Description = Utility.StaticValues.Accounting.LocalSale,
                    IsInflow = true,
                    MovementType = Utility.StaticValues.Accounting.LocalSale,
                    Sum = TotalAmount,
                    UserId = claim.Value,
                    Cleared = false,
                    
                };

                _unitOfWork.CurrentMovement.Add(currentMovement);

                _unitOfWork.Save();
              
            foreach (var orderItem in postOrderDetailList)
            {
                string[] orderDetailArray = orderItem.Split(new char[] { ',' });
                    var productId = Convert.ToInt32(orderDetailArray[3]);
                    var quantity = Convert.ToInt32(orderDetailArray[0]);
                    var price = Convert.ToDouble(orderDetailArray[1]);
                OrderDetail  orderDetail = new OrderDetail() {
                   Count = quantity,
                   OrderHeaderId = header.Id,
                   Price = price,
                   ProductName = orderDetailArray[2],
                   ProductId = productId
                };

                

               _unitOfWork.OrderDetails.Add(orderDetail);
                    _unitOfWork.Product.MinusStockQuantity(productId, quantity);
            

                    headerId = header.Id;
            }
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message});
            }
          




            return Json(new { success=true, message=headerId});
        }

        #endregion


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            if (status != null)
            {
                if (status == "complated")
                    return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: u => u.Status == StaticValue.OrderDelivered).OrderBy(u => u.OrderDate) });
                else if (status == "cancelled")
                    return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: u => u.Status == StaticValue.OrderCancelled).OrderBy(u => u.OrderDate) });
                else if (status == "inprocess")
                    return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: u => u.Status == StaticValue.OrderSubmitted || u.Status == StaticValue.OrderSubmitted).OrderBy(u => u.OrderDate) });
                else if (status == "localsale")
                    return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: u => u.Status == StaticValue.LocalSaleProcess).
                        Select(u => new { Id = u.Id,FullName = u.FullName, TotalAmount = u.TotalAmount, forSortOrderDate = u.OrderDate,OrderDate = u.OrderDate.ToShortDateString() }).
                        OrderByDescending(k => k.forSortOrderDate)
                    }) ;

                else
                    return Json(new { data = _unitOfWork.OrderHeader.GetAll().OrderBy(u => u.OrderDate) });
            }
            return Json(new { data = _unitOfWork.OrderHeader.GetAll().OrderBy(u=>u.OrderDate) });
        }

        [HttpGet]
        public IActionResult getOrderDetails(int OrderId)
        {
            return Json(new { data = _unitOfWork.OrderDetails.GetAll(filter : u => u.OrderHeaderId == OrderId) });
        }

        public IActionResult PrepareAndSendOrder(int id)
        {
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, StaticValue.OrderOnTheWay);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeliveredOrder(int id)
        {
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, StaticValue.OrderDelivered);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult PayedOrder(int id)
        {
            _unitOfWork.OrderHeader.ChangePaymentStatus(id, StaticValue.PaymentDone);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
              _unitOfWork.OrderHeader.ChangeOrderStatus(id, StaticValue.OrderCancelled);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult PayOrder(int id)
        {
            _unitOfWork.OrderHeader.ChangePaymentStatus(id, StaticValue.PaymentDone);
            return Json(new { success = true });
        }
        #endregion
    }
}