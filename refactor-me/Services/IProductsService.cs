using refactor_me.DataAccess;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Services
{
    public interface IProductsService
    {   
        Products GetAllProducts();
        Product GetProductById(Guid id);
        Products GetProductsByName(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid productId);
    }
}
