using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eroxia.model
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Material { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }

        public List<PurchaseProduct>? PurchaseProducts { get; set; } = new List<PurchaseProduct>();

        public Product(string name, string manufacturer, decimal price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }

        public Product(int productId, string name, string manufacturer, decimal price)
        {
            ProductId = productId;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }

        public override string? ToString()
        {
            return $"{Name} ({ProductId}) - {Manufacturer} - {Price:C}";
        }
    }
}
