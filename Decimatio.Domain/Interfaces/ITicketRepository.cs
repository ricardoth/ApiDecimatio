namespace Decimatio.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<long> AddTicket(Ticket ticket);
        Task<int> AddTicketQR(TicketQR ticketQR);
        Task<Ticket> GetInfoTicket(long idTicket);
        Task<IEnumerable<Ticket>> GetAllTicket();
    }
}
