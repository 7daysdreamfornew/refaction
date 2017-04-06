using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Services;
using refactor_me.Models;
using refactor_me.DataAccess;

namespace refactor_me.test
{
    [TestClass]
    public class ProductOptionsServiceTests
    {
        [TestMethod]
        public void GetProductOptionsByProductIdTest()
        {
            var productOptionsService = new ProductOptionsService();
            var output = productOptionsService.GetProductOptionsByProductId(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"));
            Assert.IsNotNull(output);
            Assert.AreNotEqual(0, output.Items.Count);
        }

        [TestMethod]
        public void GetProductOptionByIdTest()
        {
            var productOptionsService = new ProductOptionsService();
            var output = productOptionsService.GetProductOptionById(new Guid("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03"));
            Assert.IsNotNull(output);
            Assert.AreEqual(new Guid("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03"), output.Id);
        }

        [TestMethod]
        public void CreateProductOptionTest()
        {
            var productsService = new ProductsService();
            var createdDetails = productsService.GetProductsByName("Samsung");

            var productOptionsService = new ProductOptionsService();
            var input = new ProductOption
            {
                ProductId = createdDetails.Items[0].Id,
                Name = "Rose Gold",
                Description = "Rose Gold Samsung"
            };

            productOptionsService.CreateOption(input);

            var output = productOptionsService.GetProductOptionsByProductId(createdDetails.Items[0].Id);
            Assert.IsNotNull(output);
            Assert.AreEqual(createdDetails.Items[0].Id, output.Items[0].ProductId);
        }

        [TestMethod]
        public void UpdateProductOptionTest()
        {
            var productsService = new ProductsService();
            var createdProductDetails = productsService.GetProductsByName("Nokia 3310");

            var productOptionsService = new ProductOptionsService();
            var createdProductOptionDetails = productOptionsService.GetProductOptionsByProductId(createdProductDetails.Items[0].Id);

            var input = new ProductOption
            {
                Id = createdProductOptionDetails.Items[0].Id,
                ProductId = createdProductOptionDetails.Items[0].ProductId,
                Name = "White",
                Description = "White Apple"
            };

            productOptionsService.UpdateOption(input);

            var output = productOptionsService.GetProductOptionById(createdProductOptionDetails.Items[0].Id);
            Assert.IsNotNull(output);
            Assert.AreEqual("White", output.Name);
        }

        [TestMethod]
        public void DeleteProdutOptionTest()
        {
            var productsService = new ProductsService();
            var createdProductDetails = productsService.GetProductsByName("Nokia 3310");

            var productOptionsService = new ProductOptionsService();
            var createdProductOptionDetails = productOptionsService.GetProductOptionsByProductId(createdProductDetails.Items[0].Id);

            productOptionsService.DeleteOption(createdProductOptionDetails.Items[0].Id);

            var output = productOptionsService.GetProductOptionsByProductId(createdProductOptionDetails.Items[0].Id);
            Assert.IsNull(output);
        }
    }
}
