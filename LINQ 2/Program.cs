namespace LINQ_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new()
            {
                new() { ProductID = 1,  ProductName = "Chai",                   Category = "Beverages",     UnitPrice = 18m,    UnitsInStock = 39 },
                new() { ProductID = 2,  ProductName = "Chang",                  Category = "Beverages",     UnitPrice = 19m,    UnitsInStock = 17 },
                new() { ProductID = 3,  ProductName = "Aniseed Syrup",          Category = "Condiments",    UnitPrice = 10m,    UnitsInStock = 13 },
                new() { ProductID = 4,  ProductName = "Ikura",                  Category = "Seafood",       UnitPrice = 31m,    UnitsInStock = 31 },
                new() { ProductID = 5,  ProductName = "Konbu",                  Category = "Seafood",       UnitPrice = 6m,     UnitsInStock = 24 },
                new() { ProductID = 6,  ProductName = "Carnarvon Tigers",       Category = "Seafood",       UnitPrice = 62.5m,  UnitsInStock = 42 },
                new() { ProductID = 7,  ProductName = "Cote de Blaye",          Category = "Beverages",     UnitPrice = 263.5m, UnitsInStock = 17 },
                new() { ProductID = 8,  ProductName = "Tofu",                   Category = "Produce",       UnitPrice = 23.25m, UnitsInStock = 35 },
                new() { ProductID = 9,  ProductName = "Mishi Kobe Niku",        Category = "Meat",          UnitPrice = 97m,    UnitsInStock = 29 },
                new() { ProductID = 10, ProductName = "Sir Rodney's Marmalade", Category = "Confections",   UnitPrice = 81m,    UnitsInStock = 40 },
                new() { ProductID = 11, ProductName = "Queso Cabrales",         Category = "Dairy",         UnitPrice = 21m,    UnitsInStock = 22 },
                new() { ProductID = 12, ProductName = "Genen Shouyu",           Category = "Condiments",    UnitPrice = 15.5m,  UnitsInStock = 39 },
                new() { ProductID = 13, ProductName = "Pavlova",                Category = "Confections",   UnitPrice = 17.45m, UnitsInStock = 29 },
                new() { ProductID = 14, ProductName = "Alice Mutton",           Category = "Meat",          UnitPrice = 39m,    UnitsInStock = 0  },
                new() { ProductID = 15, ProductName = "Teatime Chocolate",      Category = "Confections",   UnitPrice = 9.2m,   UnitsInStock = 25 },
                new() { ProductID = 16, ProductName = "Sasquatch Ale",          Category = "Beverages",     UnitPrice = 14m,    UnitsInStock = 111 },
                new() { ProductID = 17, ProductName = "Steeleye Stout",         Category = "Beverages",     UnitPrice = 18m,    UnitsInStock = 20 },
                new() { ProductID = 18, ProductName = "Gorgonzola",             Category = "Dairy",         UnitPrice = 12.5m,  UnitsInStock = 86 },
            };

            List<Customer> customers = new()
            {
                new() { CustomerID = "ALFKI", CompanyName = "Alfreds Futterkiste",Country = "Germany",
                    Orders = new() { new() { OrderID = 1, Total = 100m }, new() { OrderID = 2, Total = 250m } } },

                new() { CustomerID = "ANATR", CompanyName = "Ana Trujillo", Country = "Mexico",
                    Orders = new() { new() { OrderID = 3, Total = 80m } } },

                new() { CustomerID = "BLAUS", CompanyName = "Blauer See Delikatessen", Country = "Germany",
                    Orders = new() { new() { OrderID = 4, Total = 300m } } },

                new() { CustomerID = "BONAP", CompanyName = "Bon app'", Country = "France",
                    Orders = new() { new() { OrderID = 5, Total = 175m } } },
            };


            #region Top 3 most expensive products
            Console.WriteLine("1. Top 3 most expensive  "); 
            var top3 = products.OrderByDescending(p => p.UnitPrice).Take(3);
            foreach (var p in top3) 
            {
                Console.WriteLine(p);
            }
            #endregion

            #region Page 2, page size = 5
            Console.WriteLine("\n 2. Page 2 (size 5) ");
            int page = 2, pageSize = 5;
            var pageItems = products.Skip((page - 1) * pageSize).Take(pageSize);
            foreach (var p in pageItems)
            {
                Console.WriteLine(p);
            }
            #endregion

            #region TakeWhile price < 25
            Console.WriteLine("\n 3. TakeWhile UnitPrice < 25 ");
            var cheap = products.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
            foreach (var p in cheap)
            {
                Console.WriteLine(p);
            }
            #endregion

            #region Are ALL Seafood products in stock?
            Console.WriteLine("\n 4. All Seafood in stock? ");
            bool allSeafoodInStock = products
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);
            Console.WriteLine(allSeafoodInStock);
            #endregion

            #region Does the ID list contain 9?
            Console.WriteLine("\n 5. ids contains 9? ");
            int[] ids = { 3, 9, 13, 18 };
            Console.WriteLine(ids.Contains(9));
            #endregion

            #region Group by Category and print each group with its product count
            Console.WriteLine("\n 6. Group by Category with count ");
            var byCategory = products.GroupBy(p => p.Category);
            foreach (var g in byCategory)
            {
                Console.WriteLine($"{g.Key} ({g.Count()} products)");
                foreach (var p in g)
                {
                    Console.WriteLine($"{p.ProductName}");
                }
            }
            #endregion

            #region Group by Category, project only product names per group
            Console.WriteLine("\n 7. Names per category ");
            var namesPerCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Names = g.Select(p => p.ProductName) });
            foreach (var g in namesPerCategory)
            {
                Console.WriteLine($"{g.Category}: {string.Join(", ", g.Names)}");
            }
            #endregion

            #region Categories that have MORE THAN 3 products
            Console.WriteLine("\n 8. Categories with > 3 products ");
            var bigCategories = products
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key);
            foreach (var c in bigCategories)
            {
                Console.WriteLine(c);
            }
            #endregion

            #region QUERY SYNTAX
            Console.WriteLine("\n 9. Customers by Country (query syntax) ");
            var byCountry =
                from c in customers
                group c by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                };
            foreach (var x in byCountry)
            {
                Console.WriteLine($"{x.Country}: Count={x.Count}, TotalOrderValue={x.TotalOrderValue}");
            }
            #endregion

            #region
            Console.WriteLine("\n 10. Total units in stock ");
            Console.WriteLine(products.Sum(p => p.UnitsInStock));
            #endregion

            #region Cheapest and most expensive prices
            Console.WriteLine("\n 11. Min / Max price ");
            Console.WriteLine($"Cheapest: {products.Min(p => p.UnitPrice)}");
            Console.WriteLine($"Most expensive: {products.Max(p => p.UnitPrice)}");
            #endregion

            #region Distinct categories
            Console.WriteLine("\n 12. Distinct categories ");
            foreach (var c in products.Select(p => p.Category).Distinct())
            {
                Console.WriteLine(c);
            }
            #endregion
        }
    }
}
