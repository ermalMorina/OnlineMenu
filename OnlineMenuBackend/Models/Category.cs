using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> ? Products { get; set; }
        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }

    }
}
