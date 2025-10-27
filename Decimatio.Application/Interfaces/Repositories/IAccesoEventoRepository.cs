using Decimatio.Domain.Entities;

namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IAccesoEventoRepository
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso);
        Task<AccesoEventoStatus> ValidarAccesoTicketExtranjero(TicketAcceso ticketAcceso);
        Task<AccesoEventoStatus> ValidarAccesoTicketFullAccess(TicketAcceso ticketAcceso);
        Task<int> RegistroAccesoEvento(AccesoEvento evento);
        Task<int> SalidaAccesoEvento(long idAccesoEvento);
        Task<IEnumerable<AccesoEventoTicket>> GetAllAccesoEventoTickets();
    }
}
