using Microsoft.CodeAnalysis;
using Models.AccountModels;
using Models.OrderModels;
using Models.ProductModels;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using Webapi.Data.DataModels;
using Webapp.Interfaces;

namespace Webapp.Services
{
    public class SrwasButikServices : ISrwasButikServices
    {
        private readonly string _baseUrl = "https://localhost:7207/api/";
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _http;

        public SrwasButikServices(HttpClient http)
        {
            _http = http;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var response = await _http.GetAsync($"{_baseUrl}product");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ProductModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<ProductModel> GetProductByName(string productName)
        {
            var response = await _http.GetAsync($"{_baseUrl}product/{productName}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ProductModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<bool> CreateProduct(CreateNewProductModel model)
        {
            try
            {
                var url = _baseUrl + "product";
                var data = JsonSerializer.Serialize(model);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
	            _http.BaseAddress = new Uri($"{_baseUrl}");

                var response = await _http.DeleteAsync($"product/{productId}/delete");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateProduct(int productId, ProductUpdateModel product)
        {
            try
            {
                var url = $"{_baseUrl}product/{productId}/update";
                var data = JsonSerializer.Serialize(product);
                var response = await _http.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AccountModel>> GetCustomers()
        {
            var response = await _http.GetAsync($"{_baseUrl}account");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<AccountModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<AccountModel> GetByEmailAddress(string EmailAddress)
        {
            var response = await _http.GetAsync($"{_baseUrl}account/{EmailAddress}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AccountModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<bool> UpdateAccount(int accountId, AccountUpdateModel account)
        {
            try
            {
                var url = $"{_baseUrl}account/{accountId}";
                var data = JsonSerializer.Serialize(account);
                var response = await _http.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<bool> CreateAccount(AccountModel account)
        {
            try
            {
                var url = _baseUrl + "account";
                var data = JsonSerializer.Serialize(account);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrderModel>> GetAllOrderDetails()
        {
            var response = await _http.GetAsync($"{_baseUrl}order/get-all-order-details");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<OrderModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<List<OrderModel>> GetOrderDetailsByAccountId(int accountId)
        {
            var response = await _http.GetAsync($"{_baseUrl}order/{accountId}/GetOrderDetailsByAccountId");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<OrderModel>>(data, _options);
                return result;
            }
            
	        throw new Exception("Det gick inget vidare");
            
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            var response = await _http.GetAsync($"{_baseUrl}product/GetCategories");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<CategoryModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<CategoryModel> GetCategoryById(int categoryId)
        {
            var response = await _http.GetAsync($"{_baseUrl}product/{categoryId}/category");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CategoryModel>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<Account?> CheckIfAccountExist(LoginModel model)
        {
            var url = _baseUrl + "account/check-if-account-exist";
            var dataInput = JsonSerializer.Serialize(model);

            var response = await _http.PostAsync(url, new StringContent(dataInput, Encoding.Default, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (data != "")
                {
	                var result = JsonSerializer.Deserialize<Account?>(data, _options);
	                return result;
				}
                else
                {
	                return null;
                }
                
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            var response = await _http.GetAsync($"{_baseUrl}product/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ProductModel>(data, _options);

                return result;
            }
            else
            {
                throw new Exception("Det gick inget vidare");
            }
        }

        public async Task<bool> CreateOrder(NewOrderInputModel newOrder)
        {
            try
            {
                var url = _baseUrl + "order";
                var data = JsonSerializer.Serialize(newOrder);

                var response = await _http.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
