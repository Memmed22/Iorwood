using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category CategoryObj { get; set; }
        public CategoryController(IUnitOfWork unitOfWork)
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
            CategoryObj = new Category();
            if (id != 0 && id != null)
            {
                CategoryObj = _unitOfWork.Category.FirstOrDefault(u => u.Id == id);
            }

            return View(CategoryObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                //update
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else return View(category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult Get()
        {
           // return Json( _unitOfWork.Category.GetAll() );
           var  k = Json(new { data = _unitOfWork.Category.GetAll() });
            return k;
        }

        [HttpDelete]
        public IActionResult Ondelete(int id)
        {
            try
            {
                //_unitOfWork.Category.Add(new Category { Name = "Geldi", OrderList = 10 });
                //_unitOfWork.Save();

                var individualCategory = _unitOfWork.Category.FirstOrDefault(u => u.Id == id);
            if (individualCategory == null)
                return Json(new { success = false, message = "Poof! Your element has not been deleted!" });

            _unitOfWork.Category.Remove(individualCategory);
            _unitOfWork.Save();
            }
            catch (Exception)
            {
                
            }
            return Json(new { success = true, message = "Poof! Your element has been deleted!" });

        }
        #endregion
    }
}