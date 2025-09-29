using Decimatio.Domain.CustomEntities;
using Decimatio.Domain.Entities;
using Decimatio.Domain.QueryFilters;

namespace Decimatio.Application.Interfaces.Services
{
    public interface IAccesoEventoService
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso);
        Task<int> SalidaAccesoEvento(long idAccesoEvento);
        Task<PagedList<AccesoEventoTicket>> GetAccesosEventosTickets(AccesoEventoTicketQueryFilter filtros);
    }
}
