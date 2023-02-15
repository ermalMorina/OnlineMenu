namespace OnlineMenu.SignalR
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public string Token { get; set; }
        public CustomAuthorizeAttribute(string token)
        {
            Token = token;
        }
    }
}
