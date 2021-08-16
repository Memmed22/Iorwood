using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IorwoodDemo.Areas.JWTAuth.Entities;
using IorwoodDemo.Areas.JWTAuth.Helpers;
using IorwoodDemo.Areas.JWTAuth.Helpers.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;



namespace IorwoodDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtAuthManager _jwtAuthManager;
        private IHttpContextAccessor _httpContextAccessor;
       
        public AccountController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> signInManager, IJwtAuthManager jwtAuthManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _jwtAuthManager = jwtAuthManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClearingCashDesk()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
               var userName =  User.Identity.Name;
                _jwtAuthManager.RemoveRefreshTokenByUsername(userName);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(string userName, string password)
        {
            try
            {
                if (userName == null || password == null)
                    return BadRequest();


                var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);

                
                if (!result.Succeeded)
                    return Unauthorized("Wrong Password");
                //asgakini islet
                //if (!passwordOK)
                //    return StatusCode(StatusCodes.Status401Unauthorized, "Incorrect username or password");

// ###########         Belke lazim olar deye
                //if (existingUser == null)
                //{
                //    response = Request.CreateResponse(HttpStatusCode.NotFound);
                //}
                //else
                //{
                //    var loginSuccess =
                //        string.Equals(EncryptPassword(model.Password, existingUser.Salt),
                //            existingUser.PasswordHash);

                var userId = _unitOfWork.UserApplication.FirstOrDefault(u => u.UserName == userName).Id;

                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Manager"),
                     new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Email, userName)
                    };

                var jwtResult = _jwtAuthManager.GenerateTokens(userName, claims, DateTime.Now);
                var UserName = User.Identity.Name;

                return Ok(new LoginResult
                {
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken= jwtResult.RefreshToken,
                    UserName = userName
                });
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message + "\n" + ex.InnerException.Message });
            }
        }

        [HttpPost]
       
        public ActionResult RefreshToken(string RefreshToken, string AccessToken) 
        {
            try
            {
                var UserName = User.Identity.Name;
                if (string.IsNullOrWhiteSpace(RefreshToken))
                    return Unauthorized();


                var jwtResult = _jwtAuthManager.Refresh(RefreshToken, AccessToken, DateTime.Now);

              

                return Ok(new LoginResult
                {
                    UserName = jwtResult.UserName,
                    RefreshToken = jwtResult.RefreshToken,
                    AccessToken = jwtResult.AccessToken
                });

            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}