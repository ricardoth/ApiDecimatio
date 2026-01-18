using Decimatio.Application.DTOs;

namespace Decimatio.Common.Interfaces
{
    public interface IEmailSenderService
    {
        Task<string> SendEmailTicket(EmailTicketDto emailTicketDto);
    }
}
