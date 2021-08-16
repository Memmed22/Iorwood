using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
   public  class AccountingBook
    {
        public int Id { get; set; }
        public DateTime AccountingDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public double InFlowSum { get; set; }

        public double OutFlowSum { get; set; }

        public double CashLeft { get; set; }

    }
}
