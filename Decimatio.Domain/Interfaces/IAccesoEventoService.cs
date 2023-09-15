namespace Decimatio.Domain.Interfaces
{
    public interface IAccesoEventoService
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso);
    }
}
