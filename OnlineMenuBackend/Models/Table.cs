namespace OnlineMenu.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        public List<Order> Orders { get; set; }
    }
}
