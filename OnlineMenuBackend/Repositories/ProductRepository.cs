using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private OMContext context;
        private ApiResponse response;
        private IValidator<ProductViewModel> _validator;

        public ModelStateDictionary ModelState { get; private set; }

        public ProductRepository(OMContext context, IValidator<ProductViewModel> validator)
        {
            this.context = context;
            _validator = validator;
        }
        public async Task<ApiResponse> DeleteProduct(Product product)
        {
            if (product == null)
            {
                return new ApiResponse(400, "This product doesn't exist");
            }

            var result = context.Products.Remove(product);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Product deleted");
        }

        public async Task<IEnumerable<Product>> GetAllProductsFromTenant()
        {
            var result = await context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetProductById(int id)
        {
            var result = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ApiResponse> AddProduct(ProductViewModel product)
        {
            ValidationResult result = await _validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                return new ApiResponse(400, result.ToString());
            }

            var fileName = Path.GetFileName(product.Photo.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Tenants/" + product.TenantId, fileName);
            if (fileName.Contains(".jpg") || fileName.Contains(".png"))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.Photo.CopyToAsync(stream);
                }

                var model = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Note = product.Note,
                    Photo = filePath,
                    TenantId = product.TenantId,
                    CategoryId = product.CategoryId
                };

                await context.Products.AddAsync(model);
                await context.SaveChangesAsync();
                return new ApiResponse(200, "Product added");
            }
            return new ApiResponse(400, "Fotot pranohen vetem ne formatin jpg ose png");
        }
        
        public async Task<ApiResponse> UpdateProduct(Product product)
        {
            if (product == null)
            {
                return new ApiResponse(400, "Product doesn't exist");
            }
            context.Update(product);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Product added");
        }
    }
}
