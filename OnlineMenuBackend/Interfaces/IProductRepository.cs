using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface IProductRepository
    {
        Task<ApiResponse> AddProduct(ProductViewModel product);
        Task<IEnumerable<Product>> GetAllProductsFromTenant();
        Task<ApiResponse> DeleteProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<ApiResponse> UpdateProduct(Product product); 
    }
}
