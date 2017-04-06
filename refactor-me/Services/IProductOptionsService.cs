using refactor_me.DataAccess;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Services
{
    public interface IProductOptionsService
    { 
        ProductOptions GetAllProductOptions();
        ProductOption GetProductOptionById(Guid productId, Guid id);
        ProductOption GetProductOptionById(Guid id);
        ProductOptions GetProductOptionsByProductId(Guid productId);
        
        void CreateOption(Guid productId, ProductOption productOption);
        void UpdateOption(ProductOption productOption);
        void DeleteOption(Guid id);
    }
}
