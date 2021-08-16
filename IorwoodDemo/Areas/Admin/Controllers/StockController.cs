using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls
        public IActionResult GetAll => Json(new { data = _unitOfWork.Product.GetAll(includeProperties: "Product") });
        #endregion
    }
}