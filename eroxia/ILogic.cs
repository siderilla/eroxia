﻿using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal interface ILogic
    {

        public Task<List<Product>> GetAllProductsAsync();

        public Task<List<Employee>> GetAllEmployeesAsync();

    }
}
