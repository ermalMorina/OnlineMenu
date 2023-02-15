namespace OnlineMenu.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}