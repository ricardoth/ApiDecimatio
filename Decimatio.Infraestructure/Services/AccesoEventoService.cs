namespace Decimatio.Infraestructure.Services
{
    public class AccesoEventoService : IAccesoEventoService
    {
        private readonly IAccesoEventoRepository _accesoEventoRepository;

        public AccesoEventoService(IAccesoEventoRepository accesoEventoRepository)
        {
            _accesoEventoRepository = accesoEventoRepository;
         }


        public async Task<AccesoEventoStatus> ValidarAccesoTicket(TicketAcceso ticketAcceso)
        {
            try
            {
                var result = await _accesoEventoRepository.ValidarAccesoTicket(ticketAcceso);
                //validar, si es ok, registrar acceso evento, sino entregar ticket invalido para acceder

                if (result.StatusCode)
                {
                    AccesoEvento accesoEvento = new();
                    accesoEvento.IdTicket = ticketAcceso.IdTicket;
                    accesoEvento.FechaHoraEntrada = DateTime.Now;

                    int accesoEventoResult = await _accesoEventoRepository.RegistroAccesoEvento(accesoEvento);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en Validar el Ticket");
            }
        }
    }
}
