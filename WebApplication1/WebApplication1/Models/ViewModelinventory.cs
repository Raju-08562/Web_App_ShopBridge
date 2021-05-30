using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ViewModelinventory
    {
        public int ItemID { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Provide Discription for the product")]
        public string Description { get; set; }

       [Range(1.0,double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue,ErrorMessage ="Quantity should be more that 0")]
        public int TotalQuantity { get; set; }
    }
}