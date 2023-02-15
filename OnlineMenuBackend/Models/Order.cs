namespace OnlineMenu.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string? Note { get; set; }
        public bool isLocal { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public bool isClosed { get; set; }
        public int? TableId { get; set; }
        public Table Table { get; set; }
        public List<ProductOrder>? ProductOrders { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}
