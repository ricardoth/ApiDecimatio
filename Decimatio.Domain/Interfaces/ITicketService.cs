namespace Decimatio.Domain.Interfaces
{
    public interface ITicketService
    {
        Task<int> AddTicket(Ticket ticket);
    }
}
