using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Model.ViewModel;
using IorwoodDemo.Utility;
using IorwoodDemo.Utility.StaticValues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CurrentMovementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrentMovementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            var a = CashDeskStaticValues.StaticValues().Where(k => k.Meta == "Front").ToList();

            return Json(new { data = a });
        }

        public IActionResult ClearingCashDesk()
        {
            CurrentMovementViewModel currentMovement = new CurrentMovementViewModel();

            var inflowAmount = _unitOfWork.CurrentMovement.GetAll(filter: u => u.Cleared == false && u.IsInflow == true).Sum(u => u.Sum);
            var outflowAmount = _unitOfWork.CurrentMovement.GetAll(filter: u => u.Cleared == false && u.IsInflow == false).Sum(u => u.Sum);
          
            currentMovement.totalInflow = (double)inflowAmount;
            currentMovement.totalOutflow= (double)outflowAmount;

            return View(currentMovement);
        }

        #region API

        [HttpPost]
        public IActionResult ClearCashDesk(CurrentMovementViewModel currentMovementVM)
        {
            if(currentMovementVM == null)
                return Json(new { success = false, data="Empty data" });
            try
            {
                var claimIntetity = (ClaimsIdentity)User.Identity;
                var claim = claimIntetity.FindFirst(ClaimTypes.NameIdentifier);

                AccountingBook accountingBook = new AccountingBook
                {
                    AccountingDate = DateTime.Now,
                    CashLeft = currentMovementVM.cashLeft,
                    InFlowSum = currentMovementVM.totalInflow,
                    OutFlowSum = currentMovementVM.totalOutflow,
                    UserId = claim.Value
                };

                _unitOfWork.AccountingBook.Add(accountingBook);
                _unitOfWork.Save();

                _unitOfWork.CurrentMovement.setAccountingBookId(accountingBook.Id);



                CurrentMovement currentMovement = new CurrentMovement()
                {
                    //AccountingBookId
                    CurrentDate = DateTime.Now,
                    Description = "After Cleared",
                    IsInflow = true,
                    MovementType =CashDeskStaticValues.StaticValues().FirstOrDefault(u=> u.Key == "CashDeskClearInFlow").Key,
                    Sum = currentMovementVM.cashLeft,
                    UserId = claim.Value,
                    Cleared = false,
                };

                _unitOfWork.CurrentMovement.Add(currentMovement);


                _unitOfWork.Save();

                return RedirectToAction("ClearingCashDesk");
               
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message });
            }
            
        }

        [HttpGet]
        public IActionResult GetUnClearedMovement() {
            var data = _unitOfWork.CurrentMovement.GetAll(filter: u => u.Cleared == false, includeProperties: "ApplicationUser").Select(u=> new { 
                user = u.ApplicationUser.UserName.Split('@')[0],
                way = (u.IsInflow == true ? "In" : "Out"),
                type = u.MovementType,// CashDeskStaticValues.StaticValues().FirstOrDefault(k=> k.Key ==  u.MovementType).Text,
                description = u.Description,
                date = u.CurrentDate.ToShortDateString(),
                sum = u.Sum
            }).OrderBy(u=> u.date);
            return Json(new { data = data });
        }

        [HttpGet]
        [Authorize]
        public IActionResult FrontEndExpenseTypes()
        {
            return Json(new { data = CashDeskStaticValues.StaticValues().
                Where(k => k.Meta == "FrontExpense").
                Select(k=> new { k.Key, k.Text }).
                ToList() });
        }

        [HttpPost]
        [Authorize]
        public IActionResult PostExpense(string expenseType, double totalAmount, string description)
        {

            if(expenseType == null || expenseType == "undefined")
                return Json(new { success = false, message = "Expense is empty" });
            try
            {

            var claimIntetity = (ClaimsIdentity)User.Identity;
            var claim = claimIntetity.FindFirst(ClaimTypes.NameIdentifier);

            

            CurrentMovement currentMovement = new CurrentMovement()
            {
                //AccountingBookId
                CurrentDate = DateTime.Now,
                Description = description,
                IsInflow = false,
                MovementType = expenseType,
                Sum = totalAmount,
                UserId = claim.Value,
                Cleared = false
            };
                _unitOfWork.CurrentMovement.Add(currentMovement);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true, message = "Refunded successfully" });
        }
        #endregion



    }
}