using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}