using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Persistence;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private OMContext context;
        private IValidator<CategoryViewModel> _validator;

        public CategoryRepository(OMContext context, IValidator<CategoryViewModel> validator)
        {
            this.context = context;
            _validator = validator;
        }

        public async Task<ApiResponse> DeleteCategory(Category category)
        {
            if (category == null)
            {
                return new ApiResponse(400, "This category doesn't exist");
            }

            var result = context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Category deleted");
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesFromTenant()
        {
            var result = await context.Categories.ToListAsync();
            return result;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var result = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ApiResponse> AddCategory(CategoryViewModel category)
        {
            ValidationResult result = await _validator.ValidateAsync(category);

            if (!result.IsValid)
            {
                return new ApiResponse(400, result.ToString());
            }
            var model = new Category
            {
                Name = category.Name,
                TenantId = category.TenantId,
            };
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Category added");
        }

        public async Task<ApiResponse> UpdateCategory(Category category)
        {
            if (category == null)
            {
                return new ApiResponse(400, "Category doesn't exist");
            }
            context.Update(category);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Category added");
        }
    }
}
