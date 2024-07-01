namespace ImmobiliareApi.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailRegistration(string toEmail, string subject, string message);
        Task NotifyAdminOfRegistration(string customerName, string customerLastName, string customerEmail);
    }
}
