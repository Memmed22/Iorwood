using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
   public class Extra : IIorwoodEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Extra Name")]
        public string Name { get; set; }
    }
}
