using IorwoodDemo.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.DataAccess.Data
{
    public class IorwoodDbContext : IdentityDbContext
    {
        public IorwoodDbContext(DbContextOptions<IorwoodDbContext> options)
           : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Extra> Extra { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Stock> Stock { get; set; }

        public DbSet<RefundHeader> RefundHeader { get; set; }
        public DbSet<RefundDetail> RefundDetail { get; set; }
        public DbSet<CurrentMovement> CurrentMovement { get; set; }

        public DbSet<AccountingBook> AccountingBook { get; set; }

        public DbSet<UserApplication> UserApplication { get; set; }

    }
}
