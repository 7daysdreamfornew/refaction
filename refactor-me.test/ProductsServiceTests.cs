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
        [TestMethod]
        public void Test1_CreateProduct()
        {
            ProductsService productsService = new ProductsService();
            Product input = new Product
            {
                Name = "Sony Xperia X Compact",
                Description = "Snapdragon 650, 64-bit Hexa-core processor",
                Price = (Decimal)699.99,
                DeliveryPrice = (Decimal)25.00
            };

            productsService.CreateProduct(input);

            var output = productsService.GetProductsByName("Sony Xperia X Compact");
            Assert.IsNotNull(output);
            Assert.AreEqual("Sony Xperia X Compact", output.Items[0].Name);
        }

        [TestMethod]
        public void Test2_UpdateProduct()
        {
            ProductsService productsService = new ProductsService();
            Products createdDetails = productsService.GetProductsByName("Sony Xperia X Compact");

            Assert.IsNotNull(createdDetails);
            Product input = new Product
            {
                Id = createdDetails.Items[0].Id,
                Name = "Sony Xperia XA",
                Description = "5-inch 720p edge-to-edge display with a slight curve",
                Price = (Decimal)569.00,
                DeliveryPrice = (Decimal)16.00,
            };

            productsService.UpdateProduct(input);

            Products output = productsService.GetProductsByName("Sony Xperia XA");
            Assert.IsNotNull(output);
            Assert.AreEqual("Sony Xperia XA", output.Items[0].Name);
        }

        [TestMethod]
        public void Test3_GetAllProductsTest()
        {
            var productsService = new ProductsService();
            var output = productsService.GetAllProducts();
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void Test4_GetProductsByNameTest()
        {
            ProductsService productsService = new ProductsService();
            Products output = productsService.GetProductsByName("Sony Xperia XA");
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output.Items.Count);
        }

        [TestMethod]
        public void Test5_GetProductByIdTest()
        {
            // Should return Apple iPhone 6S
            ProductsService productsService = new ProductsService();
            Product output = productsService.GetProductById(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"));
            Assert.IsNotNull(output);
            Assert.AreEqual(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), output.Id);
        }

        [TestMethod]
        public void Test6_DeleteProductTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdDetails = productsService.GetProductsByName("Sony Xperia XA");

            Assert.IsNotNull(createdDetails);
            productsService.DeleteProduct(createdDetails.Items[0].Id);

            Product output = productsService.GetProductById(createdDetails.Items[0].Id);
            Assert.IsNull(output);
        }
    }


}
