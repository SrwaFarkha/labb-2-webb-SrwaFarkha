using Models.Dto;
using Models.ProductModels;
using Webapi.Data.DataModels;

namespace Webapi.Data.Repositories.Interfaces
{
    public interface IProductRepository
    { 
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetByName(string name);
        Task CreateProduct(CreateProductModel product);
        Task DeleteProduct(int id);
        Task UpdateProduct(int productId, ProductUpdateModel update);
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<ProductDto> GetById(int productId);
    }
}
