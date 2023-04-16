namespace Decimatio.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<int> AddTicket(Ticket ticket);
    }
}
