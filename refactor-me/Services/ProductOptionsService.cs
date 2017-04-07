using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Services
{
    public class ProductOptionsService : IDisposable, IProductOptionsService
    {
        private DataAccess.ProductContext db = new DataAccess.ProductContext();

        public ProductOptions GetAllProductOptions()
        {
            try
            {
                List<ProductOption> options = db.ProductOptions.ToList();
                return new ProductOptions(options);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOptions GetProductOptionsByProductId(Guid productId)
        {
            try
            {                
                List<ProductOption> options = db.ProductOptions.Where(o => o.ProductId == productId).ToList();
                return new ProductOptions(options);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProductOption GetProductOptionById(Guid productId, Guid id)
        {
            try
            {
                ProductOption productOption = db.ProductOptions.FirstOrDefault(o => o.Id == id && o.ProductId == productId);
                return productOption;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public ProductOption GetProductOptionById(Guid id)
        {
            try
            {
                ProductOption productOption = db.ProductOptions.FirstOrDefault(o => o.Id == id);
                return productOption;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateOption(Guid productId, ProductOption option)
        {
            ProductOption orig = new ProductOption()
            {
                ProductId = productId,
                Name = option.Name,
                Description = option.Description
            };

            db.ProductOptions.Add(orig);
            db.SaveChanges();
        }

        public void CreateOption(ProductOption option)
        {
            var orig = new ProductOption()
            {
                ProductId = option.ProductId,
                Name = option.Name,
                Description = option.Description
            };

            db.ProductOptions.Add(orig);
            db.SaveChanges();
        }

        public void UpdateOption(ProductOption option)
        {

            var orig = new ProductOption(option.Id)
            {
                Name = option.Name,
                Description = option.Description
            };


            ProductOption optionToUpdate = GetProductOptionById(option.Id);
            if (optionToUpdate != null)
            {
                db.Entry(optionToUpdate).CurrentValues.SetValues(orig);
            }

            db.Entry(optionToUpdate).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
        }

        public void DeleteOption(Guid id)
        {
            ProductOption option = GetProductOptionById(id);
            if (option != null)
            {
                //db.Set<ProductOption>().Remove(option);
                db.Entry(option).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}