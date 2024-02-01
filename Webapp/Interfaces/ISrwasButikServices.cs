using Models.AccountModels;
using Models.OrderModels;
using Models.ProductModels;
using Webapi.Data.DataModels;

namespace Webapp.Interfaces
{
    public interface ISrwasButikServices
    {
        Task<List<ProductModel>> GetProducts();
        Task<ProductModel> GetProductByName(string productName);
        Task<ProductModel> GetProductById(int productId);
        Task<bool> CreateProduct(CreateNewProductModel model);
        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateProduct(int productId, ProductUpdateModel product);
        Task<List<AccountModel>> GetCustomers();
        Task<AccountModel> GetByEmailAddress(string EmailAddress);
        Task<bool> UpdateAccount(int accountId, AccountUpdateModel account);
        Task<bool> CreateAccount(AccountModel account);
        Task<List<OrderModel>> GetAllOrderDetails();
        Task<List<OrderModel>> GetOrderDetailsByAccountId(int accountId);

		Task<bool> CreateOrder(NewOrderInputModel newOrder);
        Task<List<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategoryById(int categoryId);
        Task<Account?> CheckIfAccountExist(LoginModel model);
    }
}
