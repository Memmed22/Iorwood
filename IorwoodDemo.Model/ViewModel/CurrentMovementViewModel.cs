using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IorwoodDemo.Model.ViewModel
{
  public  class CurrentMovementViewModel
    {
        public double totalInflow { get; set; }
        public double totalOutflow { get; set; }
        [Required(ErrorMessage ="Leave in Cash cant be empty")]
        public double cashLeft { get; set; }
        public string Description { get; set; }
    }
}
