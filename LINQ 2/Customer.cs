using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_2
{
    public class Customer
    {
        public string CustomerID { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new();
    }
}
