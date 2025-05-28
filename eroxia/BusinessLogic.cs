using eroxia.model;
using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal class BusinessLogic : ILogic
    {
        private IStorage Storage { get; set; }


        private List<Employee>? Employees { get; set; }
        private List<Product>? Products { get; set; }
        private List<Client>? Clients { get; set; }


        public BusinessLogic(IStorage storage)
        {
            Storage = storage;
        }
        public List<Employee> GetAllEmployees()
        {
            if (Employees == null)
            {
                try
                {
                    Employees = Storage.GetAllEmployeesFromDB().Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching employees: {ex.Message}");
                    Employees = new List<Employee>(); // Return an empty list on error
                }
            }
            return Employees;
        }

        public List<Product> GetAllProducts()
        {
            if (Products == null)
            {
                try 
                {
                    Products = Storage.GetAllProductsFromDB().Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching products: {ex.Message}");
                    Products = new List<Product>(); // Return an empty list on error
                }
            }
            return Products;
        }

        public bool DeleteProduct(int productId)
        {
            var result = Storage.DeleteProductFromDB(productId).Result;
            if (result)
            {
                Products?.RemoveAll(p => p.ProductId == productId);
            }
            return result;
        }

        public bool InsertProduct(Product product)
        {
            var resultId = Storage.InsertProductToDB(product).Result;
            if (resultId > 0)
            {
                product.ProductId = resultId; // Set the ProductId from the database
                Products?.Add(product);
                return true;
            }
            return false;
        }

        public List<Client> GetAllClients()
        {
            if (Clients == null)
            {
                Clients = new List<Client>();
                try
                {
                    var tempClients = Storage.GetAllClientsFromDB().Result;
                    foreach (var client in tempClients)
                    {
                        client.Employee = Employees?.Find(e => e.FiscalCode == client.FiscalCodeEmployee);
                        Clients.Add(client);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching clients: {ex.Message}");
                    Clients = new List<Client>(); // Return an empty list on error
                }
            }
            return Clients;
        }
    }
}
