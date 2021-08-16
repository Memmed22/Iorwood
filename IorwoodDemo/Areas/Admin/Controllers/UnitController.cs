using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Unit UnitObj { get; set; }
        public UnitController(IUnitOfWork unitOfWork)
        {
            UnitObj = new Unit();

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                return View(new Unit());
            }
            UnitObj = _unitOfWork.Unit.FirstOrDefault(u => u.Id == id);
            return View(UnitObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (!ModelState.IsValid)
            {
                return View(UnitObj);
            }
            if (UnitObj.Id == 0)
            {
                _unitOfWork.Unit.Add(UnitObj);
            }
            else
            {
                _unitOfWork.Unit.Update(UnitObj);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Unit.GetAll() });
        }

        [HttpDelete]
        public IActionResult OnDelete(int? id)
        {
            if (id == null)
                return Json(new { success = false, message = "" });
            var innerUnit = _unitOfWork.Unit.FirstOrDefault(u => u.Id == id);
            _unitOfWork.Unit.Remove(innerUnit);
            _unitOfWork.Save();
            return Json(new { success = true, message = "" });
        }
        #endregion
    }
}