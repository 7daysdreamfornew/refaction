using refactor_me.DataAccess;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_me.Services
{
    public class ProductsService : IDisposable, IProductsService
    {
        private ProductContext db = new ProductContext();

        public Products GetAllProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                products = db.Products.ToList();
                return new Products(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Products GetProductsByName(string name)
        {
            try
            {
                List<Product> products = db.Products.Where(p => p.Name == name).ToList();
                return new Products(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetProductById(Guid id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            Product orig = new Product(product.Id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            Product prodToUpdate = GetProductById(product.Id);

            if (prodToUpdate != null)
            {
                db.Entry(prodToUpdate).CurrentValues.SetValues(orig);
            }

            db.Entry(prodToUpdate).State = System.Data.Entity.EntityState.Modified;
            
            db.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            Product product = GetProductById(id);
            if(product != null)
            {
                //db.Set<Product>().Remove(product);
                db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            db.Products.Add(product);
            db.SaveChanges();
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