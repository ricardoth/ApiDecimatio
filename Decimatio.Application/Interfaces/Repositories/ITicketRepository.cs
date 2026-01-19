namespace Decimatio.Application.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<long> AddTicket(Ticket ticket);
        Task<int> AddTicketQR(TicketQR ticketQR);
        Task<Ticket> GetInfoTicket(long idTicket);
        Task<IEnumerable<Ticket>> GetAllTicket(TicketQueryFilter filtros);
        Task<IEnumerable<Ticket>> GetAllTicketReport();
        Task<int> GetCounterTicket();
        Task<TicketQR> GetTicketQR(long idTicket);
        Task<bool> DeleteDownTicket(long idTicket, bool activo);

        Task<bool> AddPreferenceTicket(PreferenceTicket ticket);
        Task<IEnumerable<PreferenceTicket>> GetPreferenceTicketsByTransaction(string transactionId);
        Task<bool> UpdateTicketsDownload(string transactionId);
    }
}
