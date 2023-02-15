using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface ITenantRepository
    {
        Task<ApiResponse> AddTenant(TenantViewModel tenant);
        Task<List<Tenant>> GetAllTenants();
        Task<ApiResponse> DeleteTenant(Tenant tenant);
        Task<Tenant> GetTenantById(int id);
        Task<ApiResponse> UpdateTenant(Tenant tenant);
        Task<Tenant> GetTenantFromSubdomain(string tenant);
    }
}
