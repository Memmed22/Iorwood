using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.Extensions;
using Microsoft.AspNetCore.Http;

namespace IorwoodDemo.Areas.Admin.Controllers
{

    [Area("Admin")]
  
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private readonly IorwoodDbContext _context;

        [BindProperty]
        public ProductUpsertViewModel ProductVM { get; set; }
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost, IorwoodDbContext context)
        {
            ProductVM = new ProductUpsertViewModel();
            _webHost = webHost;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM = new ProductUpsertViewModel()
            {
                Product = new Model.Entity.Product(),
                CategoryList = _unitOfWork.Category.CategoryListForDropDown().ToList(),
                ExtraList = _unitOfWork.Extra.ExtraListForDropDown().ToList(),
                UnitList = _unitOfWork.Unit.UnitListForDropDown().ToList()
            };

            if (id == null)
            {
                return View(ProductVM);
            }
            else
            {
                ProductVM.Product = _unitOfWork.Product.FirstOrDefault(u => u.Id == id);
                if (ProductVM.Product == null)
                    return NotFound();
                return View(ProductVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductUpsertViewModel ProductVM)
        {

            
            try
            {
 var files = HttpContext.Request.Form.Files;
            var webRoot = _webHost.WebRootPath.ToString();
                
               

            //Extra bos gelende IsValid olmur
            if(!ModelState.IsValid)
            {
                ProductVM.CategoryList = _unitOfWork.Category.CategoryListForDropDown().ToList();
                ProductVM.ExtraList = _unitOfWork.Extra.ExtraListForDropDown().ToList();
                ProductVM.UnitList = _unitOfWork.Unit.UnitListForDropDown().ToList();
                return View(ProductVM);
            }
            if (ProductVM.Product.Id == 0)
            {

                    //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                    //string directory = Path.Combine(webRoot + @"\images\ProductHomePage");
                    //string fullPath = Path.Combine(directory, fileName);

                    //using (FileStream fileStream = System.IO.File.Create(fullPath))
                    //{
                    //    files[0].CopyTo(fileStream);
                    //}

                    ProductVM.Product.Image = "";// @"\images\ProductHomePage\" + fileName;
                _unitOfWork.Product.Add(ProductVM.Product);
                _unitOfWork.Save();
            }
            else
            {  
              
                if (files.Count > 0)
                {
                    
                    
                    var imageDIr = webRoot + ProductVM.Product.Image.TrimStart();
                    if (System.IO.File.Exists(imageDIr))
                    {
                        System.IO.File.Delete(imageDIr);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                    string directory = Path.Combine(webRoot + @"\images\ProductHomePage");
                    string fullPath = Path.Combine(directory, fileName);

                    using (FileStream fileStream = System.IO.File.Create(fullPath))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    ProductVM.Product.Image = @"\images\ProductHomePage\" + fileName;

                }
                else
                {
                    //ProductVM.Product.Image = productFromDB.Image;
                }
                _unitOfWork.Product.Update(ProductVM.Product);
                _unitOfWork.Save();
            }

            
            }
            catch (Exception ex)
            {
                //_unitOfWork.Category.Add(new Category { Name = ex.Message , OrderList = 3 });
                //_unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }


        #region Stock Haraket
        public IActionResult StcokList(bool isImport) {
            return View();
        }

        [HttpPost]
        public IActionResult ImportStock(int id, double quantity) {
            try
            {
                _unitOfWork.Product.AddStockQuantity(id, quantity);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "Stock  Updated Fail \n" + ex.Message});
            }
             

            return Json(new { success = true, message = "Stock Successfully Updated" });
        }
        
        


        public IActionResult UpsertProductStock(int id)
        {
            ProductVM = new ProductUpsertViewModel()
            {
                Product = new Model.Entity.Product()
            };

            ProductVM.Product = _unitOfWork.Product.FirstOrDefault(u => u.Id == id);

            return View(ProductVM);
        }

        [HttpPost]
        public IActionResult UpsertProductStock()
        {
            _unitOfWork.Product.UpdateStockQuantity(ProductVM.Product.Id, ProductVM.Product.StockQuantity, ProductVM.Product.StockMinQuantity);
            return RedirectToAction(nameof(StcokList));
        }

        #endregion


        #region API CALLS

        //Buraya sepcific Columnlari ceke bilecek API elave etmek lazimdir. 

        [HttpGet]
        [Authorize]
        public IActionResult GetAll() => Json(new { data = _unitOfWork.Product.GetAll(includeProperties: "Category,Unit") });



        [HttpGet]
       [Authorize]
        public IActionResult GetAllForRefund() => Json(new { data = _unitOfWork.Product.GetAll(filter:u=> u.Active == true, includeProperties: "Category").
            Select(u=> new { Id = u.Id, Name = u.Name, Price = u.Price,Category = u.Category.Name, })});  

        public IActionResult getById(int id) {
            var a = _unitOfWork.Product.FirstOrDefault(u => u.Id == id);
            return Json(new { data = a});
        }
        
        [HttpDelete]
        public IActionResult OnDelete(int id) 
        {
            try
            {
                if (id != 0)
                {
                    var innerProduct = _unitOfWork.Product.FirstOrDefault(u => u.Id == id);
                    if (innerProduct == null)
                    {
                        return Json(new { success = false, message = "" });
                    }
                    var imageDIr = _webHost.WebRootPath.ToString() + innerProduct.Image.TrimStart();
                    if (System.IO.File.Exists(imageDIr))
                    {
                        System.IO.File.Delete(imageDIr);
                    }
                    _unitOfWork.Product.Remove(innerProduct);
                    _unitOfWork.Save();

                   
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "" });
            }
            return Json(new { success = true, message = "" });
        }

        [HttpGet]
        public IActionResult Active(int id)
        {
            _unitOfWork.Product.Activate(id);
          
            return Json(new { success = true, mesage = "Successfully activated" });
        }

        [HttpGet]
        public IActionResult Deactive(int id)
        {
            _unitOfWork.Product.Deactivate(id);
           
            return Json(new { success = true, mesage = "Successfully activated" });
        }

        #endregion
    }
}