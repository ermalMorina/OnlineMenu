namespace OnlineMenu.Viewmodels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        public int? TenantId { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
    }
}
