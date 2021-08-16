using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IorwoodDemo.Data.Migrations;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserApplicationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
     
        private readonly UserManager<IdentityUser> _userManager;


        public UserApplicationController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {


            //var uname = HttpContext.Request.Headers["username"].ToString();
            //var pass = HttpContext.Request.Headers["password"].ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, username)
            };

            var result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);
            await HttpContext.SignInAsync(new ClaimsPrincipal());

            //var aa = await _signInManager.UserManager.FindByNameAsync(username);
             
            if(result.Succeeded)
            {


                //   string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByNameAsync(username);
                

                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
                return Json(new {success = true, data = claimIdentity.Name });
            }

            return Json(new { success = false, data = "fail" });

        }

        //[HttpGet]
        //public IActionResult Upsert(int? id)
        //{
        //    UserApplication userApplication = new UserApplication();

        //    return View(userApplication);
        //}

        [HttpGet]
        public IActionResult GetAll() {
            var d = _unitOfWork.UserApplication.GetAll();
        return Json(new { data = _unitOfWork.UserApplication.GetAll()});

        }



    }
}