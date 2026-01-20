using Decimatio.Domain.CustomEntities;

namespace Decimatio.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<string> AddTicket(TicketDto ticketDto);
        Task<string> AddTickets(IEnumerable<TicketDto> ticketsDto);
        Task<(IEnumerable<TicketDto>, MetaData)> GetAllTickets(TicketQueryFilter filtros);
        Task<IEnumerable<TicketDto>> GetAllTicketsExcel(TicketQueryFilter filtros);
        Task<TicketQRDto> GetTicketQR(int idTicket);
        Task<TicketQRDto> GetTicketVoucherPDF(int idTicket);
        Task<bool> DeleteDownTicket(long idTicket, bool activo);
        Task<IEnumerable<PreferenceTicketDto>> GetAllPreferenceTickets();
        Task<IEnumerable<PreferenceTicketDto>> GetPreferenceTicketsByTransaction(string transactionId);
        Task<string> AddQueueTicket(string preferenceCode);
    }
}
