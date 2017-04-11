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
        public void Test9_CreateProductOptionTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdDetails = productsService.GetProductsByName("Sony Xperia X Compact");

            ProductOptionsService productOptionsService = new ProductOptionsService();
            ProductOption input = new ProductOption
            {
                ProductId = createdDetails.Items[0].Id,
                Name = "White",
                Description = "White Sony"
            };

            productOptionsService.CreateOption(input);

            ProductOptions output = productOptionsService.GetProductOptionsByProductId(createdDetails.Items[0].Id);
            Assert.IsNotNull(output);
            Assert.AreEqual(createdDetails.Items[0].Id, output.Items[0].ProductId);
        }

        [TestMethod]
        public void Test10_UpdateProductOptionTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdProductDetails = productsService.GetProductsByName("Sony Xperia X Compact");

            ProductOptionsService productOptionsService = new ProductOptionsService();
            ProductOptions createdProductOptionDetails = productOptionsService.GetProductOptionsByProductId(createdProductDetails.Items[0].Id);

            // Update first option
            ProductOption input = new ProductOption
            {
                Id = createdProductOptionDetails.Items[0].Id,
                ProductId = createdProductOptionDetails.Items[0].ProductId,
                Name = "Red",
                Description = "Red Sony"
            };

            productOptionsService.UpdateOption(input);

            ProductOption output = productOptionsService.GetProductOptionById(createdProductOptionDetails.Items[0].Id);
            Assert.IsNotNull(output);
            // Should fail the test here
            Assert.AreEqual("White", output.Name);
        }
        [TestMethod]
        public void Test7_GetProductOptionByIdTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdDetails = productsService.GetProductsByName("Sony Xperia X Compact");
            ProductOptionsService productOptionsService = new ProductOptionsService();

            ProductOption output = productOptionsService.GetProductOptionById(createdDetails.Items[0].Id);
            Assert.IsNotNull(output);
            Assert.AreEqual(new Guid("5c2996ab-54ad-4999-92d2-89245682d534"), output.Id);
        }

        [TestMethod]
        public void Test8_GetProductOptionsByProductIdTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdDetails = productsService.GetProductsByName("Sony Xperia X Compact");
            ProductOptionsService productOptionsService = new ProductOptionsService();
        
            ProductOptions output = productOptionsService.GetProductOptionsByProductId(createdDetails.Items[0].Id);
            Assert.IsNotNull(output);
            Assert.AreNotEqual(0, output.Items.Count);
        }
        
     

        [TestMethod]
        public void Test11_DeleteProdutOptionTest()
        {
            ProductsService productsService = new ProductsService();
            Products createdProductDetails = productsService.GetProductsByName("Sony Xperia X Compact");

            ProductOptionsService productOptionsService = new ProductOptionsService();
            ProductOptions createdProductOptionDetails = productOptionsService.GetProductOptionsByProductId(createdProductDetails.Items[0].Id);

            // Delete the first option
            productOptionsService.DeleteOption(createdProductOptionDetails.Items[0].Id);

            ProductOptions output = productOptionsService.GetProductOptionsByProductId(createdProductOptionDetails.Items[0].Id);
            Assert.IsNull(output);
        }
    }
}
