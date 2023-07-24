namespace Decimatio.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailTicketDto emailDto);
    }
}
