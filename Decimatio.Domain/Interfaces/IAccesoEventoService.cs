namespace Decimatio.Domain.Interfaces
{
    public interface IAccesoEventoService
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso);
        Task<int> SalidaAccesoEvento(long idAccesoEvento);
        Task<PagedList<AccesoEventoTicket>> GetAccesosEventosTickets(AccesoEventoTicketQueryFilter filtros);
    }
}
