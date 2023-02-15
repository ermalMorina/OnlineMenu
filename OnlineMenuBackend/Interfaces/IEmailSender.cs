namespace OnlineMenu.Interfaces
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
