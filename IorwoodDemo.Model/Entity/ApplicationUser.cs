using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
   public class ApplicationUser : IdentityUser
    {
        [Required]
        public string  Name { get; set; }
        public string Adress { get; set; }
    }
}
