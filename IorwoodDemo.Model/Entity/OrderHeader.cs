using IorwoodDemo.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IorwoodDemo.Model.Entity
{
    public class OrderHeader: IIorwoodEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public double TotalAmount { get; set; }
        
        [Display(Name ="Pick Up Time")]
        [NotMapped]
        public DateTime PickupTime { get; set; }

        [Required]
        [Display(Name = "Pick Up Date")]
        [NotMapped]
        public DateTime PickUpDate { get; set; }
        
        public DateTime PickUpDateTime { get; set; }

        public DateTime OrderDate { get; set; }
        public string Comment { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }

    }
}
