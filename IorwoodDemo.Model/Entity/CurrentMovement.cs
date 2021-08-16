using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    //Hele DB elave edilmeyib, yeni teleb ola biler deye
    public class CurrentMovement: IIorwoodEntity
    {
        public int  Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public  ApplicationUser ApplicationUser { get; set; }

        public int? AccountingBookId { get; set; }
        [ForeignKey("AccountingBookId")]
        public virtual AccountingBook AccountingBook { get; set; }

        public bool IsInflow { get; set; }
        public string MovementType { get; set; }
        public double Sum { get; set; }
        public string Description { get; set; }
        public bool Cleared { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
