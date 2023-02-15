namespace OnlineMenu.Viewmodels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }
        //public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public List <string> Roles {get; set;}
    }
}
