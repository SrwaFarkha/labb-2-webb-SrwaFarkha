using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.ProductModels;
using Webapi.Controllers;
using Webapi.Data.DataModels;
using Webapi.Data.Repositories.Interfaces;

namespace Webapi.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;

        public ProductRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _dbContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductDescription = x.ProductDescription,
                    Price = x.Price,
                    CategoryName = x.Category.Name,
                    Discontinued = x.Discontinued
                }).ToListAsync();

            return products;
        }

        public async Task<ProductDto> GetByName(string name)
        {
            var product = await _dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductName ==  name);

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                CategoryName = product.Category.Name,
                Discontinued = product.Discontinued
            };

            return productDto;
        }

        public async Task<ProductDto> GetById(int productId)
        {
            var product = await _dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == productId);
            
            var productDto = new ProductDto
            {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    CategoryName = product.Category.Name,
                    Discontinued = product.Discontinued
            };

	        return productDto;
        }

        public async Task CreateProduct(CreateProductModel product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                CategoryId = product.Category.Id,
                Discontinued = false,
            };

            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(int productId, ProductUpdateModel update)
        {
            var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (productFromDb != null)
            {
                productFromDb.ProductName = update.ProductName;
                productFromDb.ProductDescription = update.ProductDescription;
                productFromDb.Price = update.Price;
                productFromDb.Discontinued = update.Discontinued;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            return category;
        }
    } 
}
