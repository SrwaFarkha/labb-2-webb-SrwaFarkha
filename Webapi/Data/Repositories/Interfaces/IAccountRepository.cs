using Models.AccountModels;
using Models.Dto;
using Webapi.Data.DataModels;

namespace Webapi.Data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
	    Task<List<Account>> GetAll();
	    Task<Account> GetByEmailAddress(string EmailAddress);

		Task UpdateAccount(int accountId, AccountUpdateModel customer);
        Task CreateAccount(AccountModel account);

        Task<Account?> CheckIfAccountExist(LoginModel input);
    }
}
