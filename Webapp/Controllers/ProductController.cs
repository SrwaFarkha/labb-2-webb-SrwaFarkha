using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.AccountModels;
using Models.ProductModels;
using Webapi.Data.DataModels;
using Webapp.Extensions;
using Webapp.Interfaces;
using Webapp.Models;

namespace Webapp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISrwasButikServices _srwasButikServices;

        public ProductController(ISrwasButikServices srwasButikServices)
        {
            _srwasButikServices = srwasButikServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _srwasButikServices.GetProducts();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _srwasButikServices.GetCategories();
            if (categories != null)
            {
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            }

            var model = new CreateNewProductModel();
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewProductModel data, int SelectedCategoryId)
        {
            var category = await _srwasButikServices.GetCategoryById(SelectedCategoryId);

            var newProduct = new CreateNewProductModel
            {
                ProductName = data.ProductName,
                ProductDescription = data.ProductDescription,
                Price = data.Price,
                Category = category,
                Discontinued = false
            };

            try
            {
                if (await _srwasButikServices.CreateProduct(newProduct))
                {
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                return View("Error");
            }

            return View("Error");
        }

        public async Task<IActionResult> Delete(int productId)
        {
            if (await _srwasButikServices.DeleteProduct(productId)) return RedirectToAction("Index");
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _srwasButikServices.GetProductById(productId);
            var model = new ProductUpdateModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Discontinued = product.Discontinued
            };
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateModel model)
        {
            try
            {
	            if (await _srwasButikServices.UpdateProduct(model.ProductId, model))
                {                 
                    return RedirectToAction("Index");
                }
                return View("Error");
            }

            catch (System.Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Search(string productName)
        {
            var products = await _srwasButikServices.GetProducts();
            foreach (var item in products)
            {
                if (item.ProductName == productName)
                {
                    var product = await _srwasButikServices.GetProductByName(productName);

                    var productList = new List<ProductModel>();
                    productList.Add(product);
                    return View("Index", productList);
                }
            }
            return View("NotFound");
        }


        public async Task<IActionResult> AddToCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductCartModel>>("shoppingCart") ?? new List<ProductCartModel>();
            
            var product = await _srwasButikServices.GetProductById(productId);
            cart.Add(new ProductCartModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                CategoryName = product.CategoryName,
                Discontinued = product.Discontinued,
                Quantity = 1,
            });

            HttpContext.Session.SetObjectAsJson("shoppingCart", cart);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductCartModel>>("shoppingCart") ?? new List<ProductCartModel>();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("shoppingCart", cart);
            }
            return RedirectToAction("ShowShoppingCart");
        }

        public async Task<IActionResult> ShowShoppingCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductCartModel>>("shoppingCart") ?? new List<ProductCartModel>();

            var products = cart
                .GroupBy(x => x.ProductName)
                .Select(g => new ProductCartModel
                {
                    ProductId = g.First().ProductId,
                    ProductName = g.First().ProductName,
                    ProductDescription = g.First().ProductDescription,
                    Price = g.First().Price,
                    Quantity = g.Select(x => x.Quantity).Sum()
                }).ToList();


            decimal totalPrice = 0;
            foreach (var product in products)
            {
                totalPrice += product.Price * product.Quantity;
            }
            ViewBag.TotalPrice = totalPrice;

            return View("ShoppingCart", products);
        }
    }
}
