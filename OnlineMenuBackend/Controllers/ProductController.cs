using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository _repository)
        {
            repository = _repository;
        }
        [HttpGet("getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var prod = await repository.GetAllProductsFromTenant();
            return Ok(prod);
        }
      
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            var result = await repository.AddProduct(product);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("deleteproduct")]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            await repository.DeleteProduct(product);
            return Ok();
        }

        [HttpGet("getproductbyid")]
        public async Task<IActionResult> GetProductById(int id)
        {
            await repository.GetProductById(id);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("updateproduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await repository.UpdateProduct(product);
            return Ok();
        }
    }
}
