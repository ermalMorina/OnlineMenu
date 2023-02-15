using System.ComponentModel.DataAnnotations;

namespace OnlineMenu.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        public int? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ProductOrder> ProductOrders { get; set; }


    }
}
