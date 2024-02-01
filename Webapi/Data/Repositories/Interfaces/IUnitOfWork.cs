namespace Webapi.Data.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
		IAccountRepository AccountRepository { get; }
		IOrderRepository OrderRepository { get; }
		IProductRepository ProductRepository { get; }
	}
}
