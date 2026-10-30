using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_2
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public override string ToString() => $"{ProductID} - {ProductName} ({Category}) ${UnitPrice} stock:{UnitsInStock}";
    }
}
