using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class Bag
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Price { get; set; }

        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public Category Category { get; set; } // nagivation property
        public Supplier Supplier { get; set; } // nagivation property
    }
}
