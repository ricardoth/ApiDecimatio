using Decimatio.Domain.CustomEntities;

namespace Decimatio.Domain.Interfaces
{
    public interface ITicketService
    {
        Task<string> AddTicket(Ticket ticket);
        Task<string> AddTickets(IEnumerable<Ticket> tickets);
        Task<PagedList<Ticket>> GetAllTickets(TicketQueryFilter filtros);
    }
}
