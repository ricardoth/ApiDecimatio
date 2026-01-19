namespace ApiDecimatio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketScannerController : ControllerBase
    {
        private readonly IAccesoEventoService _accesoEventoService;
        private readonly IMapper _mapper;

        public TicketScannerController(IAccesoEventoService accesoEventoService, IMapper mapper)
        {
            _accesoEventoService = accesoEventoService;
            _mapper = mapper;
        }

        [HttpPost("ValidarAccesoTicket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarAccesoTicket(TicketAccesoDto ticketAccesoDto)
        {
            var ticketAcceso = _mapper.Map<TicketAcceso>(ticketAccesoDto);
            var result = await _accesoEventoService.ValidarAccesoTicket(ticketAcceso);
            var response = new ApiResponse<AccesoEventoStatus>(result);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] AccesoEventoTicketQueryFilter filtros)
         {
            var ticketsAccesos = await _accesoEventoService.GetAccesosEventosTickets(filtros);
            if (ticketsAccesos == null)
                return BadRequest();

            var metaData = new MetaData
            {
                TotalCount = ticketsAccesos.TotalCount,
                PageSize = ticketsAccesos.PageSize,
                CurrentPage = ticketsAccesos.CurrentPage,
                TotalPages = ticketsAccesos.TotalPages,
                HasNextPage = ticketsAccesos.HasNextPage,
                HasPreviousPage = ticketsAccesos.HasPreviousPage,
                NextPageUrl = "",
                PreviousPageUrl = "",
            };

            var ticketsAccesoDto = _mapper.Map<IEnumerable<AccesoEventoTicketDto>>(ticketsAccesos);
            var response = new ApiResponse<IEnumerable<AccesoEventoTicketDto>>(ticketsAccesoDto)
            {
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(response);
        }

        [HttpPut("SalidaAccesoEvento")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SalidaAccesoEvento(long idAccesoEvento)
        {
            if (idAccesoEvento == 0)
                return NotFound("No se encuentra el elemento en la bd");

            var result = await _accesoEventoService.SalidaAccesoEvento(idAccesoEvento);
            var response = new ApiResponse<int>(result);
            return Ok(response);
        }
    }
}
