
using System.Threading.Tasks;
public interface ITenantResolutionStrategy
{
    Task<string> GetTenantIdentifierAsync();
}
