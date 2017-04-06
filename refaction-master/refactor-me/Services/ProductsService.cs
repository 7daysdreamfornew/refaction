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

        protected Product MapProduct(SqlDataReader rdr)
        {
            return new Product
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };
        }

        public Products GetAllProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                IQueryable<Product> test = db.Products.Select(x => x);

                var rdr = db.ExecuteReader(db.Products.ToString());
                while (rdr.Read())
                {
                    products.Add(MapProduct(rdr));
                }
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
            var orig = new Product(product.Id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };
            orig.Save();
        }

        public void DeleteProduct(Guid id)
        {
            var product = new Product(id);
            product.Delete();
        }

        public void CreateProduct(Product product)
        {
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