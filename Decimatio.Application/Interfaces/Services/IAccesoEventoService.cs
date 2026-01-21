namespace Decimatio.Application.Interfaces.Services
{
    public interface IAccesoEventoService
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAccesoDto ticketAcceso);
        Task<int> SalidaAccesoEvento(long idAccesoEvento);
        Task<(IEnumerable<AccesoEventoTicketDto>, MetaData)> GetAccesosEventosTickets(AccesoEventoTicketQueryFilter filtros);
    }
}
