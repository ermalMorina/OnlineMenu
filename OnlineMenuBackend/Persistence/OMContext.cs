
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenants.Fx;
using OnlineMenu.Models;
using OnlineMenu.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

public class OMContext : IdentityDbContext<ApplicationUser>
{
    private readonly int _tenantId;

    public OMContext(
        DbContextOptions<OMContext> options,
        IHttpContextAccessor contextAccessor) : base(options)
    {
        var currentTenant = contextAccessor.HttpContext?.GetTenant();
        _tenantId = currentTenant?.TenantId ?? 0;

        this.Filter<Category>(f => f.Where(q => q.TenantId == _tenantId));
        this.Filter<Product>(f => f.Where(q => q.TenantId == _tenantId));
        this.Filter<ProductOrder>(f => f.Where(q => q.TenantId == _tenantId));
        this.Filter<Order>(f => f.Where(q => q.TenantId == _tenantId));
        this.Filter<ApplicationUser>(f => f.Where(q => q.TenantId == _tenantId));
        this.Filter<Table>(f => f.Where(q => q.TenantId == _tenantId));
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.TenantId = _tenantId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
