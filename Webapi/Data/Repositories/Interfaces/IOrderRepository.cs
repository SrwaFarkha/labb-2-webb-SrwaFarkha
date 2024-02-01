using Models.Dto;
using Models.OrderModels;

namespace Webapi.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
	    Task<List<OrderDto>> GetOrderDetailsByAccountId(int accountId);

		Task<List<OrderDto>> GetAllOrderDetails();
		Task CreateOrder(NewOrderInputModel newNewOrder);

    }
}
