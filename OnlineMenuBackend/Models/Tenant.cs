using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Models
{
    public class Tenant
    {
        public Tenant(int tenantId, string identifier)
        {
            TenantId = tenantId;
            Identifier = identifier;
        }

        public int TenantId { get; set; }
        public string Identifier { get; set; }
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
        public List<ProductOrder>? ProductOrders { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Category>? Categories { get; set; }

    }
}