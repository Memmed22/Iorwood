using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.Repository.Abstract;
using IorwoodDemo.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Repository.Concrete
{
    internal class UserApplicationRepository:Repository<ApplicationUser>, IUserApplicationRepository
    {
        private readonly IorwoodDbContext _context;

        public UserApplicationRepository(IorwoodDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
