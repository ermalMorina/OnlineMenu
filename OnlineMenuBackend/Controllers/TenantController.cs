using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    public class TenantController : Controller
    {
        private ITenantRepository repository;

        public TenantController(ITenantRepository _repository)
        {
            repository = _repository;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [HttpPost("addtenant")]
        public async Task<IActionResult> AddTenant(TenantViewModel tenant)
        {
            var result = await repository.AddTenant(tenant);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [HttpPost("deletetenant")]
        public async Task<IActionResult> DeleteTenant(Tenant tenant)
        {
            await repository.DeleteTenant(tenant);
            return Ok();
        }
        
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [HttpGet("gettenantbyid")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            await repository.GetTenantById(id);
            return Ok();
        }
        
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [HttpPost("updatetenant")]
        public async Task<IActionResult> UpdateTenant(Tenant tenant)
        {
            await repository.UpdateTenant(tenant);
            return Ok();
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [HttpGet("gettenantfromsubdomain")]
        public async Task<IActionResult> GetTenantFromSubdomain(string tenant)
        {
            var tenants = await repository.GetTenantFromSubdomain(tenant);
            return Ok(tenants);
        }
    }
}
