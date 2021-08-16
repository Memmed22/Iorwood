using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.Model.Entity;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IorwoodDemo.DataAccess.Initializer
{
    public class DBInitializer : IDbInitializer
    {

        private readonly IorwoodDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(IorwoodDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            //if (!_db.Roles.Any(u => u.Name == StaticValue.Admin))
            //{
            //    _roleManager.CreateAsync(new IdentityRole(StaticValue.Admin)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(StaticValue.Manager)).GetAwaiter().GetResult();
            //}

            //var userName = "admin@gmail.com";

            //if (_db.ApplicationUser.Any(u => u.UserName == userName))
            //    return;



            //_userManager.CreateAsync(new ApplicationUser
            //{
            //    UserName = userName,
            //    Email = userName,
            //    Name = "Admin",
            //    Adress = "iormuganlo",
            //    EmailConfirmed = true,
            //    PhoneNumber = "555555555"

            //}, "B@rcelon@22").GetAwaiter().GetResult();


            //ApplicationUser _user = _db.ApplicationUser.Where(u => u.UserName == userName).FirstOrDefault();
            //_userManager.AddToRoleAsync(_user, StaticValue.Admin).GetAwaiter().GetResult();
        }
    }
}
