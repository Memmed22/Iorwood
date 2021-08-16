using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class Unit:IIorwoodEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Unit Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Unit Number")]
        public int Number { get; set; }

        [NotMapped]
        public string NumberWithName { get { return Number + " " + Name; }  }
    }
}
