using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models.AccountModels;
using Models.OrderModels;
using Models.ProductModels;
using Webapp.Extensions;
using Webapp.Interfaces;
using Webapp.Models;

namespace Webapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISrwasButikServices _srwasButikServices;

        public AccountController(ISrwasButikServices srwasButikServices)
        {
            _srwasButikServices = srwasButikServices;
        }

        public async Task<IActionResult> Index()
        {
            var accountEmailAddress = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var accountInfo = await _srwasButikServices.GetByEmailAddress(accountEmailAddress);
            
            return View(accountInfo);
        }


        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductCartModel>>("shoppingCart") ?? new List<ProductCartModel>();
            var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountId = Convert.ToInt32(accountIdString);

            var orderInput = new NewOrderInputModel
            {
	            AccountId = accountId,
	            OrderDate = DateTime.Now,
	            OrderDetails = cart
                    .GroupBy(x => x.ProductId)
                    .Select(g => new OrderDetailsInputModel
                {
                    ProductId = g.First().ProductId,
                    Quantity = g.Select(x => x.Quantity).Sum()
                })
            };

            await _srwasButikServices.CreateOrder(orderInput);
            HttpContext.Session.SetObjectAsJson("shoppingCart", new List<ProductCartModel>());

			return View("OrderCreated");
        }

        [HttpGet]
        public async Task<IActionResult> StartCreateAccount(RegisterAccountModel model)
        {
	        if (!ModelState.IsValid)
	        {
		        return View("RegisterAccount", model);
	        }

			return View("RegisterAccount", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount1(RegisterAccountModel data)
        {
            if (data != null)
            {
                var newCustomer = new AccountModel
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    EmailAddress = data.EmailAddress,
                    Password = data.Password,
                    PhoneNumber = data.PhoneNumber,
                    City = data.City,
                    StreetAddress = data.StreetAddress,
                    Address = new AddressModel
                    {
	                    City = data.City,
	                    StreetAddress = data.StreetAddress
                    }
                };
                await _srwasButikServices.CreateAccount(newCustomer);
            }

            var accountInfo = await _srwasButikServices.GetByEmailAddress(data.EmailAddress);
            var loginModel = new LoginModel
	            { EmailAddress = accountInfo.EmailAddress, Password = accountInfo.Password };

			return RedirectToAction("Login", "Login", loginModel );
        }

        [HttpGet]
        public async Task<IActionResult> ShowUsersOrder()
        {
	        var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ;
            var accountId = Convert.ToInt32(accountIdString);
	        var orderModel = await _srwasButikServices.GetOrderDetailsByAccountId(accountId); //service till api call
	       
	        return View("Orders", orderModel);
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllAccounts()
        {
	        var accountsModel = await _srwasButikServices.GetCustomers();
	        return View("Customers", accountsModel );
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllOrders()
        {
            var orders = await _srwasButikServices.GetAllOrderDetails();
            return View("AllOrders", orders);
        }

        [HttpPost]
        public async Task<IActionResult> StartUpdateAccount(AccountModel model)
        {
	        var accountIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
	        var accountId = Convert.ToInt32(accountIdString);

	        var updateAccountModel = new AccountUpdateModel
	        {
		        FirstName = model.FirstName,
		        LastName = model.LastName,
		        PhoneNumber = model.PhoneNumber,
		        Password = model.Password,
		        Address = new AddressModel
		        {
			        City = model.Address.City,
			        StreetAddress = model.Address.StreetAddress
		        }
	        };

	        await _srwasButikServices.UpdateAccount(accountId, updateAccountModel);
	        var accountEmailAddress = User.FindFirstValue(ClaimTypes.Email) ?? "";
	        var accountInfo = await _srwasButikServices.GetByEmailAddress(accountEmailAddress);

			return View("Index", accountInfo);
        }


        [HttpPost]
        public async Task<IActionResult> Search(string customerEmail)
        {
            var customers = await _srwasButikServices.GetCustomers();
            var customerList = new List<AccountModel>();
			foreach (var item in customers)
            {
                if (item.EmailAddress == customerEmail)
                {
                    var customer = await _srwasButikServices.GetByEmailAddress(customerEmail);

                    
                    customerList.Add(customer);
                    return View("Customers", customerList);
                }
            }
            return View("Customers", customerList);
        }
    }
}
