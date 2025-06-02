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
    internal class BusinessLogic(IStorage storage) : ILogic
    {
        private IStorage Storage { get; set; } = storage;


        private List<Employee>? Employees { get; set; }
        private List<Product>? Products { get; set; }
        private List<Client>? Clients { get; set; }
        private List<Purchase>? Purchases { get; set; }
        private List<PurchaseProduct>? PurchaseProducts { get; set; }

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
                    // CARICA GLI EMPLOYEE PRIMA!
                    if (Employees == null)
                        Employees = Storage.GetAllEmployeesFromDB().Result;

                    var tempClients = Storage.GetAllClientsFromDB().Result;

                    foreach (var temp in tempClients)
                    {
                        var employee = Employees?.Find(e => e.FiscalCode == temp.FiscalCodeEmployee);
                        var client = new Client(temp.FiscalCode, temp.Name, temp.Surname, temp.Address, employee);
                        Clients.Add(client);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching clients: {ex}");
                    Clients = new List<Client>();
                }
            }
            return Clients;
        }

        public List<Purchase> GetAllPurchases()
        {
            if (Purchases == null)
            {
                try
                {
                    Purchases = Storage.GetAllPurchasesFromDB().Result;

                    var clients = GetAllClients();

                    foreach (var purchase in Purchases)
                    {
                        var client = clients.Find(c => c.FiscalCode == purchase.FiscalCodeClient);
                        if (client != null)
                        {
                            purchase.Client = client;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching purchases: {ex.Message}");
                    Purchases = new List<Purchase>();
                }
            }

            return Purchases;
        }



        public List<PurchaseProduct> GetAllPurchaseProducts()
        {
            if (PurchaseProducts == null)
            {
                try
                {
                    // Step 1: carico dal DB
                    PurchaseProducts = Storage.GetAllPurchaseProductsFromDB().Result;

                    // Step 2: prendo gli oggetti già caricati
                    var purchases = GetAllPurchases(); // questo carica anche i client
                    var products = GetAllProducts();

                    // Step 3: associo i riferimenti
                    foreach (var pp in PurchaseProducts)
                    {
                        pp.Purchase = purchases.Find(p => p.PurchaseId == pp.PurchaseId);
                        pp.Product = products.Find(p => p.ProductId == pp.ProductId);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching PurchaseProducts: {ex.Message}");
                    PurchaseProducts = new List<PurchaseProduct>();
                }
            }

            return PurchaseProducts;
        }


    }
}
