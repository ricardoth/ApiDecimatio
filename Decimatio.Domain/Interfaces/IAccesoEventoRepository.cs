namespace Decimatio.Domain.Interfaces
{
    public interface IAccesoEventoRepository
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAccesoDto ticketAccesoDto);
        Task<AccesoEvento> RegistroAccesoEvento(AccesoEventoDto evento);
    }
}
