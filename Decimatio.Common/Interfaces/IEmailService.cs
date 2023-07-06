namespace Decimatio.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string toAddress, string subject, string body, string pdfBase64);
    }
}
