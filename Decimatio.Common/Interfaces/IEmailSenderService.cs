using Decimatio.Common.DTOs;
namespace Decimatio.Common.Interfaces
{
    public interface IEmailSenderService
    {
        Task<string> SendEmailTicket(RequestEmailTicketDto emailTicketDto);
    }
}
