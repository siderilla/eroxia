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
    }
}
