namespace Decimatio.Infraestructure.Services
{
    public class AccesoEventoService : IAccesoEventoService
    {
        private readonly IAccesoEventoRepository _accesoEventoRepository;

        public AccesoEventoService(IAccesoEventoRepository accesoEventoRepository)
        {
            _accesoEventoRepository = accesoEventoRepository;
         }

        public async Task<IEnumerable<AccesoEventoTicket>> GetAccesosEventosTickets()
        {
         
            var result =  await _accesoEventoRepository.GetAllAccesoEventoTickets();
            if (result == null)
                throw new Exception("No se pudo obtener la lista de accesos al evento");
            return result;
        }

        public async Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso)
        {
            try
            {
                var result = await _accesoEventoRepository.ValidarAccesoTicket(ticketAcceso);

                AccesoEvento accesoEvento = new();
                accesoEvento.IdTicket = ticketAcceso.IdTicket;
                accesoEvento.IdEstadoTicket = result.StatusCode;
                accesoEvento.FechaHoraEntrada = DateTime.Now;

                int accesoEventoResult = await _accesoEventoRepository.RegistroAccesoEvento(accesoEvento);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en Validar el Ticket");
            }
        }
    }
}
