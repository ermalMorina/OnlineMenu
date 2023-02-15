using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Persistence;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;
using System.Xml.Linq;

namespace OnlineMenu.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private TenantAdminDbContext context;
        private OMContext _context;
        private IValidator<TenantViewModel> _validator;

        public TenantRepository(TenantAdminDbContext context,OMContext context1, IValidator<TenantViewModel> validator)
        {
            this.context = context;
            _validator = validator;
            _context = context1;
        }

        public async Task<ApiResponse> DeleteTenant(Tenant tenant)
        {
            if (tenant == null)
            {
                return new ApiResponse(400, "This tenant doesn't exist");
            }

            var result = context.Tenants.Remove(tenant);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Tenant deleted");
        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            var result = await context.Tenants.ToListAsync();
            return result;
        }

        public async Task<Tenant> GetTenantById(int id)
        {
            var result = await context.Tenants.FirstOrDefaultAsync(x => x.TenantId == id);
            return result;
        }

        public async Task<ApiResponse> AddTenant(TenantViewModel tenant)
        {
            ValidationResult result = await _validator.ValidateAsync(tenant);

            if (!result.IsValid)
            {
                return new ApiResponse(400, result.ToString());
            }



            var model = new Tenant(tenant.TenantId, tenant.Identifier);
            await _context.Tenants.AddAsync(model);
             _context.SaveChangesAsync();
            await context.Tenants.AddAsync(model);
            await context.SaveChangesAsync();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Tenants", model.TenantId.ToString());
            Directory.CreateDirectory(path);


            return new ApiResponse(200, "Tenant added");
        }

        public async Task<ApiResponse> UpdateTenant(Tenant tenant)
        {
            if (tenant == null)
            {
                return new ApiResponse(400, "Tenant doesn't exist");
            }
            context.Update(tenant);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Tenant added");
        }

        public async Task<Tenant> GetTenantFromSubdomain(string tenant)
        {
            var tenants = await _context.Tenants.FirstOrDefaultAsync(x => x.Name.Equals(tenant));
            return tenants;
        }
    }
}

