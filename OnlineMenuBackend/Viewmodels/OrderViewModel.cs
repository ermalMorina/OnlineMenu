using OnlineMenu.Models;

namespace OnlineMenu.Viewmodels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string? Note { get; set; }
        public bool isLocal { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public bool isClosed { get; set; }
        public int? TableId { get; set; }
        public double Price { get; set; }
        public List<ProductOrder> Products { get; set; }
    }
}
