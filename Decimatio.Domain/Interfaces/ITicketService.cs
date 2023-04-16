namespace Decimatio.Domain.Interfaces
{
    public interface ITicketService
    {
        Task<string> AddTicket(Ticket ticket);
    }
}
