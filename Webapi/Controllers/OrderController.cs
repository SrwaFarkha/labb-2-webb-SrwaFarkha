using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.OrderModels;
using Webapi.Data.DataModels;
using Webapi.Data.Repositories;
using Webapi.Data.Repositories.Interfaces;

namespace Webapi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
		private readonly IUnitOfWork _uow;

		public OrderController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet("{accountId:int}/GetOrderDetailsByAccountId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderDetailsByAccountId(int accountId)
        {
            var result = await _uow.OrderRepository.GetOrderDetailsByAccountId(accountId);
            return Ok(result);
        }

        [HttpGet("get-all-order-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var result = await _uow.OrderRepository.GetAllOrderDetails();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder(NewOrderInputModel newOrder)
        {
            await _uow.OrderRepository.CreateOrder(newOrder);
            
            return Ok();
        }
    }
}
