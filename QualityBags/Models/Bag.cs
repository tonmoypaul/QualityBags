using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class Bag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }

        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        public Category Category { get; set; } // nagivation property
        public Supplier Supplier { get; set; } // nagivation property
    }
}
