namespace Decimatio.Domain.Interfaces
{
    public interface IAccesoEventoRepository
    {
        Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso);
        Task<int> RegistroAccesoEvento(AccesoEvento evento);
        Task<int> SalidaAccesoEvento(long idAccesoEvento);
        Task<IEnumerable<AccesoEventoTicket>> GetAllAccesoEventoTickets(); 
    }
}
