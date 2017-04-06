using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using refactor_me.DataAccess;

namespace refactor_me.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products(List<Product> items)
        {
            Items = items;
        }
    }
}