
using OnlineMenu.Models;
using System.Threading.Tasks;
public interface ITenantStore<T> where T : Tenant
{
    Task<T> GetTenantAsync(string identifier);
}