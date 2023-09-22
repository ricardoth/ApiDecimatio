namespace Decimatio.Infraestructure.Services
{
    public class AccesoEventoService : IAccesoEventoService
    {
        private readonly IAccesoEventoRepository _accesoEventoRepository;
        private readonly PaginationOptions _paginationOptions;

        public AccesoEventoService(IAccesoEventoRepository accesoEventoRepository, IOptions<PaginationOptions> paginationOptions)
        {
            _accesoEventoRepository = accesoEventoRepository;
            _paginationOptions = paginationOptions.Value;
         }

        public async Task<PagedList<AccesoEventoTicket>> GetAccesosEventosTickets(AccesoEventoTicketQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;


            var result =  await _accesoEventoRepository.GetAllAccesoEventoTickets();
            if (result == null)
                throw new Exception("No se pudo obtener la lista de accesos al evento");


            if (filtros.IdTicket > 0)
                result = result.Where(x => x.IdTicket == filtros.IdTicket);

            if (filtros.IdUsuario > 0)
                result = result.Where(x => x.IdUsuario == filtros.IdUsuario);

            if (filtros.IdEstadoTicket > 0)
                result = result.Where(x => x.IdEstadoTicket == filtros.IdEstadoTicket);

            if (filtros.FechaHoraEntrada.HasValue)
                result = result.Where(x => x.FechaHoraEntrada == filtros.FechaHoraEntrada);

            if (filtros.FechaHoraSalida.HasValue)
                result = result.Where(x => x.FechaHoraSalida == filtros.FechaHoraSalida);

            var pagedList = PagedList<AccesoEventoTicket>.Create(result, filtros.PageNumber, filtros.PageSize);
            return pagedList;
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
