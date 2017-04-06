using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Services;
using refactor_me.DataAccess;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductOptionsController : ApiController
    {
        private IProductOptionsService _productOptionsService;

        public ProductOptionsController(IProductOptionsService productOptionsService)
        {
            _productOptionsService = productOptionsService;
        }


        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            ProductOptions options = _productOptionsService.GetProductOptionsByProductId(productId);
            if (options == null)
            {
                return NotFound();
            }
            return this.Ok(options);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            ProductOption option = _productOptionsService.GetProductOptionById(productId, id);
            if (option == null)
            {
                return NotFound();
            }
            return this.Ok(option);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productOptionsService.CreateOption(productId, option);
            return CreatedAtRoute("DefaultApi", new { id = option.Id }, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid id, ProductOption optionUpdate)
        {
            ProductOption option = _productOptionsService.GetProductOptionById(id);
            if (option == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productOptionsService.UpdateOption(optionUpdate);
            return this.Ok();
        }



        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid id)
        {
            ProductOption option = _productOptionsService.GetProductOptionById(id);
            if (option == null)
            {
                return NotFound();
            }

            _productOptionsService.DeleteOption(id);
            return Ok();
        }
    }
}
