using Webapi.Data.Repositories.Interfaces;

namespace Webapi.Data.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _dbContext;

		public UnitOfWork(DataContext _dbContext)
		{
			this._dbContext = _dbContext;
		}

		public IAccountRepository AccountRepository =>
			new AccountRepository(_dbContext);
		public IOrderRepository OrderRepository =>
			new OrderRepository(_dbContext);
		public IProductRepository ProductRepository =>
			new ProductRepository(_dbContext);
	
	}
}
