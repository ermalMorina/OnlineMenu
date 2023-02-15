using Microsoft.AspNetCore.Identity;
using OnlineMenu.Models;

namespace OnlineMenu.Persistence
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
