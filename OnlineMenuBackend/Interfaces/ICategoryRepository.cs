using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ApiResponse> AddCategory(CategoryViewModel category);
        Task<IEnumerable<Category>> GetAllCategoriesFromTenant();
        Task<ApiResponse> DeleteCategory(Category category);
        Task<Category> GetCategoryById(int id);
        Task<ApiResponse> UpdateCategory(Category category);
    }
}
