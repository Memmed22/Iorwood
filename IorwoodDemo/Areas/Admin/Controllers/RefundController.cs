using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Utility.StaticValues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RefundController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefundController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region API CALLS
        [HttpPost]
        //array gelmesini denemem lazim. Specific item adi ile cagirmagla

        public IActionResult SubmitRefund(string postRefundHeaderString, string[] postRefundDetail)
        {
            if (postRefundHeaderString == null)
                return Json(new { success = false, message = "Refund is empty" });
            try
            {

                
                var claimIntetity = (ClaimsIdentity)User.Identity;
                var claim = claimIntetity.FindFirst(ClaimTypes.NameIdentifier);

                string[] refundHeaderArr = postRefundHeaderString.Split(new char[] { ',' });

                double totalAmount = Convert.ToDouble(refundHeaderArr[1]);
                RefundHeader refundHeader = new RefundHeader()
                {
                    OrderHeaderId = Convert.ToInt32(refundHeaderArr[0]),
                    TotalAmount = totalAmount,
                    Comment = refundHeaderArr[2],
                    RefundDate = DateTime.Now
                };

                _unitOfWork.RefundHeader.Add(refundHeader);


                CurrentMovement currentMovement = new CurrentMovement()
                {
                    //AccountingBookId
                    CurrentDate = DateTime.Now,
                    Description = Utility.StaticValues.Accounting.Refund,
                    IsInflow = false,
                    MovementType =(string)CashDeskStaticValues.StaticValues().FirstOrDefault(u => u.Key == "Refund").Key,  //bUnu yeni elave etdim test etmedim
                    Sum = totalAmount,
                    UserId = claim.Value,
                    Cleared = false,

                };

                _unitOfWork.CurrentMovement.Add(currentMovement);

                _unitOfWork.Save();




                foreach (var item in postRefundDetail)
                {
                    string[] detailArr = item.Split(new char[] { ',' });
                    int productId = Convert.ToInt32(detailArr[0]);
                    int quantity = Convert.ToInt32(detailArr[2]);
                    RefundDetail refundDetail = new RefundDetail()
                    {
                        RefundHeaderId = refundHeader.Id,
                        ProductId = productId,
                        Price = Convert.ToDouble(detailArr[1]),
                        Count = quantity
                    };

                    _unitOfWork.RefundDetail.Add(refundDetail);
                    _unitOfWork.Product.AddStockQuantity(productId, quantity);
                }

                _unitOfWork.Save();

                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new {success = true, message = "Refunded successfully" });
        }
        #endregion
    }
}
