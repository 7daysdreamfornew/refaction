using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Services;
using refactor_me.DataAccess;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// GET /products - gets all products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            Products products = _productsService.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
            return this.Ok(products);
        }

        /// <summary>
        /// GET /products?name={name} - finds all products matching the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            Products products = _productsService.GetProductsByName(name);
            if (products == null)
            {
                return NotFound();
            }
            return this.Ok(products);
        }

        /// <summary>
        /// GET /products/{id} - gets the project that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid Id)
        {
            Product product = _productsService.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }
            return this.Ok(product);
        }

        /// <summary>
        /// PUT /products/{id} - updates a product.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="productUpdate"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid Id, Product productUpdate)
        {
            Product product = _productsService.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productsService.UpdateProduct(productUpdate);
            return this.Ok();
        }

        /// <summary>
        /// POST /products - creates a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productsService.CreateProduct(product);
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        /// <summary>
        /// DELETE /products/{id} - deletes a product and its options.
        /// </summary>
        /// <param name="Id"></param>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid Id)
        {
            Product product = _productsService.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }

            _productsService.DeleteProduct(Id);
            return Ok();
        }


        //[Route("{id}")]
        //[HttpPut]
        //public void Update(Guid id, Product product)
        //{
        //    var orig = new Product(id)
        //    {
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        DeliveryPrice = product.DeliveryPrice
        //    };

        //    if (!orig.IsNew)
        //        orig.Save();
        //}

        //[Route("{id}")]
        //[HttpDelete]
        //public void Delete(Guid id)
        //{
        //    var product = new Product(id);
        //    product.Delete();
        //}

        //[Route("{productId}/options")]
        //[HttpGet]
        //public ProductOptions GetOptions(Guid productId)
        //{
        //    return new ProductOptions(productId);
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpGet]
        //public ProductOption GetOption(Guid productId, Guid id)
        //{
        //    var option = new ProductOption(id);
        //    if (option.IsNew)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return option;
        //}

        //[Route("{productId}/options")]
        //[HttpPost]
        //public void CreateOption(Guid productId, ProductOption option)
        //{
        //    option.ProductId = productId;
        //    option.Save();
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpPut]
        //public void UpdateOption(Guid id, ProductOption option)
        //{
        //    var orig = new ProductOption(id)
        //    {
        //        Name = option.Name,
        //        Description = option.Description
        //    };

        //    if (!orig.IsNew)
        //        orig.Save();
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpDelete]
        //public void DeleteOption(Guid id)
        //{
        //    var opt = new ProductOption(id);
        //    opt.Delete();
        //}




        //[Route]
        //[HttpGet]
        //public Products GetAll()
        //{
        //    return new Products();
        //}

        //[Route]
        //[HttpGet]
        //public Products SearchByName(string name)
        //{
        //    return new Products(name);
        //}

        //[Route("{id}")]
        //[HttpGet]
        //public Product GetProduct(Guid id)
        //{
        //    var product = new Product(id);
        //    if (product.IsNew)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return product;
        //}

        //[Route]
        //[HttpPost]
        //public void Create(Product product)
        //{
        //    product.Save();
        //}

        //[Route("{id}")]
        //[HttpPut]
        //public void Update(Guid id, Product product)
        //{
        //    var orig = new Product(id)
        //    {
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        DeliveryPrice = product.DeliveryPrice
        //    };

        //    if (!orig.IsNew)
        //        orig.Save();
        //}

        //[Route("{id}")]
        //[HttpDelete]
        //public void Delete(Guid id)
        //{
        //    var product = new Product(id);
        //    product.Delete();
        //}

        //[Route("{productId}/options")]
        //[HttpGet]
        //public ProductOptions GetOptions(Guid productId)
        //{
        //    return new ProductOptions(productId);
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpGet]
        //public ProductOption GetOption(Guid productId, Guid id)
        //{
        //    var option = new ProductOption(id);
        //    if (option.IsNew)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return option;
        //}

        //[Route("{productId}/options")]
        //[HttpPost]
        //public void CreateOption(Guid productId, ProductOption option)
        //{
        //    option.ProductId = productId;
        //    option.Save();
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpPut]
        //public void UpdateOption(Guid id, ProductOption option)
        //{
        //    var orig = new ProductOption(id)
        //    {
        //        Name = option.Name,
        //        Description = option.Description
        //    };

        //    if (!orig.IsNew)
        //        orig.Save();
        //}

        //[Route("{productId}/options/{id}")]
        //[HttpDelete]
        //public void DeleteOption(Guid id)
        //{
        //    var opt = new ProductOption(id);
        //    opt.Delete();
        //}
    }
}
