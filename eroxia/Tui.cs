using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal class Tui
    {

        private ILogic Logic { get; set; }
        public Tui(ILogic logic)
        {
            Logic = logic;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Eroxia!");
            while (true)
            {
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. View all products");
                Console.WriteLine("2. View all employees");
                Console.WriteLine("3. Exit");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ViewAllProducts();
                        break;
                    case "2":
                        ViewAllEmployees();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private void ViewAllEmployees()
        {
            throw new NotImplementedException();
        }

        private void ViewAllProducts()
        {
            var products = Logic.GetAllProductsAsync().Result;
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products available.");
            }
            else
            {
                Console.WriteLine("Available Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"- {product.Name} (Price: {product.Price})");
                }
            }
        }
    }
}