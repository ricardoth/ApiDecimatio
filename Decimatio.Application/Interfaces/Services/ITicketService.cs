using Decimatio.Domain.CustomEntities;
using Decimatio.Domain.QueryFilters;

namespace Decimatio.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<string> AddTicket(Ticket ticket);
        Task<string> AddTickets(IEnumerable<Ticket> tickets);
        Task<PagedList<Ticket>> GetAllTickets(TicketQueryFilter filtros);
        Task<IEnumerable<Ticket>> GetAllTicketsExcel(TicketQueryFilter filtros);
        Task<TicketQR> GetTicketQR(int idTicket);
        Task<TicketQR> GetTicketVoucherPDF(int idTicket);
        Task<bool> DeleteDownTicket(long idTicket, bool activo);
        Task<IEnumerable<PreferenceTicket>> GetAllPreferenceTickets();
        Task<IEnumerable<PreferenceTicket>> GetPreferenceTicketsByTransaction(string transactionId);
        Task<string> AddQueueTicket(string preferenceCode);
    }
}
