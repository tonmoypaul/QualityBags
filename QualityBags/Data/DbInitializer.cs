using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBags.Models;

namespace QualityBags.Data
{
    public class DbInitializer
    {
        public static void Initialize(QbDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students
            if (context.Bags.Any())
            {
                return; // DB has been seeded
            }

            var Categories = new Category[]
            {
                new Category{Name="Leather Bags"},
                new Category{Name="Backpacks"},
                new Category{Name="Laptop Bags"},
                new Category{Name="Briefcases"}
            };

            foreach (Category c in Categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();


            var Suppliers = new Supplier[]
            {
                new Supplier{Name="ABC International", Phone="0123456", Email="abc@abcint.com"},
                new Supplier{Name="BD Bags Ltd", Phone="2356899", Email="hello@bdbags.com.bd"},
                new Supplier{Name="Thai Bags", Phone="58745214", Email="support@thaibags.co.th"}
            };

            foreach (Supplier s in Suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();


            var Bags = new Bag[]
            {
                new Bag{Name="Crocodile Leather Bag", Description="This is an awesome bag", Price=300.5m, CategoryID=1, SupplierID=3},
                new Bag{Name="Bangladeshi Cow Leather Bag", Description="This is another awesome bag", Price=200.5m, CategoryID=1, SupplierID=2},
                new Bag{Name="A Very Good Bag for backpackers", Description="This is a cool backpack", Price=107.5m, CategoryID=2, SupplierID=1},
                new Bag{Name="Carry your laptop in it", Description="This is a smart laptop bag", Price=150.5m, CategoryID=3, SupplierID=2},
                new Bag{Name="Professional choice", Description="Everyday office need", Price=220.5m, CategoryID=4, SupplierID=1}
            };

            foreach (Bag b in Bags)
            {
                context.Bags.Add(b);
            }
            context.SaveChanges();

        }
    }
}
