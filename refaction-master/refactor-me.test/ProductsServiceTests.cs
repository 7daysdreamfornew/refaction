using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Services;
using refactor_me.Models;
using refactor_me.DataAccess;

namespace refactor_me.test
{
    [TestClass]
    public class ProductsServiceTests
    {
        #region Positive Tests
        [TestMethod]
        public void GetAllProductsTest()
        {
            var productsService = new ProductsService();
            var output = productsService.GetAllProducts();
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void GetProductsByNameTest()
        {
            var productsService = new ProductsService();
            var output = productsService.GetProductsByName("Samsung");
            Assert.IsNotNull(output);
            Assert.AreNotEqual(0, output.Items.Count);
        }

        [TestMethod]
        public void GetProductByIdTest()
        {
            var productsService = new ProductsService();
            var output = productsService.GetProductById(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"));
            Assert.IsNotNull(output);
            Assert.AreEqual(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), output.Id);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            var productsService = new ProductsService();
            var input = new Product
            {
                Name = "Nokia",
                Description = "Best phone ever",
                Price = 14576.99M,
                DeliveryPrice = 45.98M
            };

            productsService.CreateProduct(input);

            var output = productsService.GetProductsByName("Nokia");
            Assert.IsNotNull(output);
            Assert.AreEqual("Nokia", output.Items[0].Name);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            var productsService = new ProductsService();
            var createdDetails = productsService.GetProductsByName("Nokia");

            Assert.IsNotNull(createdDetails);
            var input = new Product
            {
                Id = createdDetails.Items[0].Id,
                Name = "Nokia 3310",
                Description = "Best phone ever",
                Price = 14576.99M,
                DeliveryPrice = 45.98M
            };

            productsService.UpdateProduct(input);

            var output = productsService.GetProductsByName("Nokia 3310");
            Assert.IsNotNull(output);
            Assert.AreEqual("Nokia 3310", output.Items[0].Name);
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            var productsService = new ProductsService();
            var createdDetails = productsService.GetProductsByName("Nokia 3310");

            Assert.IsNotNull(createdDetails);
            productsService.DeleteProduct(createdDetails.Items[0].Id);

            var output = productsService.GetProductById(createdDetails.Items[0].Id);
            Assert.IsNull(output);
        }

        #endregion
    }


}
