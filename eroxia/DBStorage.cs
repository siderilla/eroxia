using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql; // Ensure you have the Npgsql package installed for PostgreSQL access

namespace eroxia
{

    internal class DBStorage: IStorage
    {

        public static string PostGresConnectionString { get; } = "Host=localhost;Port=5432;Database=eroxia;Username=postgres;Password=superpippo";

        public async Task<List<Product>> GetAllProductsFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(PostGresConnectionString);

            var dataSource = dataSourceBuilder.Build();

            var conn = await dataSource.OpenConnectionAsync();

            var query = new NpgsqlCommand("SELECT id_product, name, manufacturer, price, material, color FROM product", conn);

            var reader = query.ExecuteReader();

            var products = new List<Product>();

            while (reader.Read())
            {
                var product = new Product(
                    reader.GetInt32(0), // ProductId
                    reader.GetString(1), // Name
                    reader.GetString(2), // Manufacturer
                    reader.GetDecimal(3) // Price
                )
                {
                    Material = reader.IsDBNull(4) ? null : reader.GetString(4), // Material
                    Color = reader.IsDBNull(5) ? null : reader.GetString(5) // Color
                };
                products.Add(product);
            }
            return products;
        }

        public async Task<List<Employee>> GetAllEmployeesFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(PostGresConnectionString);
            var dataSource = dataSourceBuilder.Build();
            var conn = await dataSource.OpenConnectionAsync();
            var query = new NpgsqlCommand("SELECT fiscal_code, name, surname, dob FROM employee", conn);
            var reader = query.ExecuteReader();
            var employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee(
                    reader.GetString(0), // EmployeeId
                    reader.GetString(1), // Name
                    reader.GetString(2), // Surname
                    reader.GetDateTime(3) // Dob
                );
                employees.Add(employee);
            }
            return employees;
        }

        public async Task<bool> DeleteProductFromDB(int productId)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(PostGresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var conn = await dataSource.OpenConnectionAsync();

            var queryString = $@"BEGIN;
                                DELETE FROM purchase_product WHERE id_product = @productId;
                                DELETE FROM product WHERE id_product = @productId;
                                COMMIT;";

            using var query = new NpgsqlCommand(queryString, conn);

            query.Parameters.AddWithValue("productId", productId); //in questo modo evitiamo SQL injection che potrebbero compromettere la sicurezza del database

            var reader = query.ExecuteReader();

            if (reader.RecordsAffected > 0)
            {
                return true; // Product deleted successfully
            }
            else
            {
                return false; // No product found with the given ID
            }

        }

        public async Task<int> InsertProductToDB(Product product)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(PostGresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var conn = await dataSource.OpenConnectionAsync();

            var queryString = $@"INSERT INTO product(name, material, manufacturer, price, color) 
                                VALUES (@name, @material, @manufacturer, @price, @color)
                                RETURNING id_product";

            using var query = new NpgsqlCommand(queryString, conn);

            query.Parameters.AddWithValue("name", product.Name);
            query.Parameters.AddWithValue("material", String.IsNullOrEmpty(product.Material) ? DBNull.Value : product.Material);
            query.Parameters.AddWithValue("manufacturer", product.Manufacturer);
            query.Parameters.AddWithValue("price", product.Price);
            query.Parameters.AddWithValue("color", String.IsNullOrEmpty(product.Color) ? DBNull.Value : product.Color);

            object? resultId = null;
            try
            {
                resultId = await query.ExecuteScalarAsync();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error inserting product: {ex.Message}");
                throw;
            }

            // Ensure a value is returned
            if (resultId != null && int.TryParse(resultId.ToString(), out int productId))
            {
                return productId;
            }
            else
            {
                throw new InvalidOperationException("Failed to insert product and retrieve its ID.");
            }
        }

        public Task<List<Client>> GetAllClientsFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(PostGresConnectionString);
            var dataSource = dataSourceBuilder.Build();
            var conn = dataSource.OpenConnectionAsync().Result;
            var query = new NpgsqlCommand("SELECT fiscal_code, name, surname, address FROM client", conn);
            var reader = query.ExecuteReader();
            var clients = new List<Client>();
            while (reader.Read())
            {
                var client = new Client(
                    reader.GetString(0), // ClientId
                    reader.GetString(1), // Name
                    reader.GetString(2), // Surname
                    reader.GetString(3) // Address
                );
                clients.Add(client);
            }
            return Task.FromResult(clients);
        }
    }
}
