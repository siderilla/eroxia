using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal interface IStorage
    {
        Task<List<Product>> GetAllProductsFromDB();

        Task<List<Employee>> GetAllEmployeesFromDB();

        Task<bool> DeleteProductFromDB(int productId);

        Task<int> InsertProductToDB(Product product);

        Task<List<Client>> GetAllClientsFromDB();
    }
}