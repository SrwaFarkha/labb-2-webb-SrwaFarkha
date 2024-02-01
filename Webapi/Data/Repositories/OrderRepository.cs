using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.OrderModels;
using Webapi.Data.DataModels;
using Webapi.Data.Repositories.Interfaces;

namespace Webapi.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dbContext;

        public OrderRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDto>> GetOrderDetailsByAccountId(int accountId)
        {
            var result = await _dbContext.Orders
	            .Include(x => x.Account)
	            .ThenInclude(x => x.Address)
	            .Include(x => x.OrderDetails)
	            .ThenInclude(x => x.Product)
	            .ThenInclude(x => x.Category)
	            .Where(x => x.AccountId == accountId)
	            .Select(x => new OrderDto
	            {
		            Account = new AccountDto
		            {
			            AccountId = x.Account.AccountId,
			            FirstName = x.Account.FirstName,
			            LastName = x.Account.LastName,
			            EmailAddress = x.Account.EmailAddress,
			            PhoneNumber = x.Account.PhoneNumber,

			            City = x.Account.Address.City,
			            StreetAddress = x.Account.Address.StreetAddress

		            },
                    OrderDate = x.OrderDate,
		            OrderDetails = x.OrderDetails.Select(x => new OrderDetailsDto
		            {
			            ProductId = x.ProductId,
			            ProductName = x.Product.ProductName,
			            Quantity = x.Quantity,
			            Price = x.Product.Price,
                        TotalProductsPrice = x.Product.Price*x.Quantity

		            }).ToList()
	            }).ToListAsync();

            return result;
        }

        public async Task<List<OrderDto>> GetAllOrderDetails()

		{
            var result = await _dbContext.Orders
                .Include(x => x.Account)
                .ThenInclude(x => x.Address)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Select(x => new OrderDto
                {
                    Account = new AccountDto
                    {
                        AccountId = x.Account.AccountId,
                        FirstName = x.Account.FirstName,
                        LastName = x.Account.LastName,
                        EmailAddress = x.Account.EmailAddress,
                        PhoneNumber = x.Account.PhoneNumber,

                        City = x.Account.Address.City,
                        StreetAddress = x.Account.Address.StreetAddress
                    },
                    OrderDetails = x.OrderDetails.Select(x => new OrderDetailsDto
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.ProductName,
                        Quantity = x.Quantity,
                        Price = x.Product.Price,
                        TotalProductsPrice = x.Product.Price * x.Quantity

					}).ToList()
                }).ToListAsync();

            return result;

        }

        public Task CreateOrder(NewOrderInputModel newNewOrder)
        {
	        var order = new Order
            {
                OrderDate = newNewOrder.OrderDate,
                AccountId = newNewOrder.AccountId,
                OrderDetails = new List<OrderDetails>()
            };

            foreach (var detail in newNewOrder.OrderDetails)
            {
                var product = _dbContext.Products.Find(detail.ProductId);
                var orderDetail = new OrderDetails
                {
                    ProductId = product.ProductId,
                    Quantity = detail.Quantity,
                };

                order.OrderDetails.Add(orderDetail);
            }

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return Task.FromResult("Order successfully created!");
        }
    }
}
