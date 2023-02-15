using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet("getallCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var prod = await repository.GetAllCategoriesFromTenant();
            return Ok(prod);
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory(CategoryViewModel category)
        {
            var result = await repository.AddCategory(category);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deletecategory")]
        public async Task<IActionResult> DeleteCategory(Category category)
        {
            await repository.DeleteCategory(category);
            return Ok();
        }

        [HttpGet("getcategorybyid")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            await repository.GetCategoryById(id);
            return Ok();
        }

        [HttpPost("updatecategory")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await repository.UpdateCategory(category);
            return Ok();
        }
    }
}
