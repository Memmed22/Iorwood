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
    public class ExtraController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public Extra ExtraObj { get; set; }
        public ExtraController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id) 
        {
            if (id == null || id == 0)
            { 
              ExtraObj = new Extra();
            }
            else
               ExtraObj = _unitOfWork.Extra.FirstOrDefault(u => u.Id == id);

            return View(ExtraObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Extra extra)
        {
            if (!ModelState.IsValid)
            {
                return View(extra);
            }
            if (extra.Id == 0)
            {
                _unitOfWork.Extra.Add(extra);
            }
            else
            {
                _unitOfWork.Extra.Update(extra);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            return Json(new { data = _unitOfWork.Extra.GetAll() });
        }

        [HttpDelete]
        public IActionResult Ondelete(int? id)
        {
            if (id == null || id == 0)
                return Json(new { success = false, message = "" });
            var innerExtra = _unitOfWork.Extra.FirstOrDefault(u => u.Id == id);
            _unitOfWork.Extra.Remove(innerExtra);
            _unitOfWork.Save();
            return Json(new { success = true, message = "" });

        }
        #endregion
    }
}