using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.ProductModels;
using Webapi.Data;
using Webapi.Data.DataModels;
using Webapi.Data.Repositories.Interfaces;

namespace Webapi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private readonly IUnitOfWork _uow;

		public ProductController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <IActionResult> GetProducts()
        {
            var data = await _uow.ProductRepository.GetAll();

            return Ok(data);
        }

        [HttpGet("{name}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct(string name)
        {
            var data = await _uow.ProductRepository.GetByName(name);
            return Ok(data);
        }

        [HttpGet("{productId:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var data = await _uow.ProductRepository.GetById(productId);
            return Ok(data);
        }
    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProduct(CreateProductModel product)
        {
            await _uow.ProductRepository.CreateProduct(product);
            return Ok();
        }

        [HttpDelete("{productId}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _uow.ProductRepository.DeleteProduct(productId);
            return Ok();
        }


        [HttpPut("{productId:int}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct(int productId, ProductUpdateModel product)
        {
            await _uow.ProductRepository.UpdateProduct(productId, product);
            return Ok();
        }

        [HttpGet("GetCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _uow.ProductRepository.GetCategories();
            return Ok(data);
        }

        [HttpGet("{categoryId:int}/category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var data = await _uow.ProductRepository.GetCategoryById(categoryId);
            return Ok(data);
        }

    }

}
