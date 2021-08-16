using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class UserApplication:IdentityUser
    {

        public string Name { get; set; }

        public string Surname { get; set; }
        [NotMapped]
        public string FullName { get { return Name + " " + Surname; } }
    }
}
