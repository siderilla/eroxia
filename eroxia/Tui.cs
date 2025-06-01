using eroxia.model;
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
                Console.WriteLine("3. View all clients");
                Console.WriteLine("4. View all purchases");
                Console.WriteLine("5. Insert product");
                Console.WriteLine("6. Delete product");
                Console.WriteLine("7. Exit");
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
                        ViewAllClients();
                        break;
                    case "4":
                        ViewAllPurchaseProducts();
                        break;
                    case "5":
                        InsertProduct();
                        break;
                    case "6":
                        DeleteProduct();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private void ViewAllProducts()
        {
            var products = Logic.GetAllProducts();
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products available.");
            }
            else
            {
                Console.WriteLine("Available Products:");
                foreach (var product in products)
                {
                    Console.WriteLine(product.ToString());
                }
            }
        }

        private void ViewAllEmployees()
        {
            var employees = Logic.GetAllEmployees();
            if (employees == null || !employees.Any())
            {
                Console.WriteLine("No employees available.");
            }
            else
            {
                Console.WriteLine("Available Employees:");
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.ToString());
                }
            }
        }


        private void ViewAllClients()
        {
            var clients = Logic.GetAllClients();
            if (clients == null || !clients.Any())
            {
                Console.WriteLine("No clients available.");
            }
            else
            {
                Console.WriteLine("Available Clients:");
                foreach (var client in clients)
                {
                    Console.WriteLine(client.ToString());
                }
            }
        }

        private void ViewAllPurchaseProducts()
        {
            var purchaseproducts = Logic.GetAllPurchaseProducts();
            if (purchaseproducts == null || !purchaseproducts.Any())
            {
                Console.WriteLine("No purchases available.");
            }
            else
            {
                Console.WriteLine("Available Purchases:");
                foreach (var purchaseproduct in purchaseproducts)
                {
                    Console.WriteLine(purchaseproduct.ToString());
                }
            }
        }


        private void InsertProduct()
        {
            Console.WriteLine("Enter product name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            var priceInput = Console.ReadLine();
            Console.WriteLine("Enter product manufacturer:");
            var manufacturer = Console.ReadLine();
            Console.WriteLine("Enter product material (optional):");
            var material = Console.ReadLine();
            Console.WriteLine("Enter product color (optional):");
            var color = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal price))
            {
                var product = new Product(0, name, manufacturer, decimal.Parse(priceInput))
                {
                    Material = string.IsNullOrEmpty(material) ? null : material,
                    Color = string.IsNullOrEmpty(color) ? null : color
                };
                // Assuming Logic has a method to insert a product
                var success = Logic.InsertProduct(product);
                if (success) {
                    Console.WriteLine($"Product '{name}' with price {price} inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to insert product. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid price. Please try again.");
            }
        }

        private void DeleteProduct()
        {
            Console.WriteLine("Enter the ID of the product to delete:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int productId))
            {
                var success = Logic.DeleteProduct(productId);
                if (success)
                {
                    Console.WriteLine($"Product with ID {productId} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete product with ID {productId}. It may not exist.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID. Please try again.");
            }
        }
    }
}