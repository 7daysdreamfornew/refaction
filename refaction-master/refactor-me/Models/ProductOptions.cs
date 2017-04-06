using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using refactor_me.Models;
using refactor_me.DataAccess;

namespace refactor_me.Models
{
    public class ProductOptions
    {
        public List<ProductOption> Items { get; private set; }

        public ProductOptions(List<ProductOption> items)
        {
            Items = items;
        }
    }
}