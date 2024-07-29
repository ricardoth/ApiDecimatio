namespace Decimatio.Domain.Interfaces
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

        Task<IEnumerable<PreferenceTicket>> GetPreferenceTicketsByTransaction(string transactionId);
    }
}
