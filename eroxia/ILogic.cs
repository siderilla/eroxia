using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal interface ILogic
    {

        public List<Product> GetAllProducts();

        public List<Employee> GetAllEmployees();

        bool DeleteProduct(int productId);

        bool InsertProduct(Product product);

        public List<Client> GetAllClients();
    }
}
