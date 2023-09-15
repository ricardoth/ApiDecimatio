namespace Decimatio.Infraestructure.Repositories
{
    public class AccesoEventoRepository : IAccesoEventoRepository
    {
        public async Task<AccesoEvento> RegistroAccesoEvento(AccesoEventoDto evento)
        {
            throw new NotImplementedException();
        }

        public async Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAccesoDto ticketAccesoDto)
        {
            throw new NotImplementedException();
        }
    }
}
