namespace ApiDecimatio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketScannerController : ControllerBase
    {
        private readonly IAccesoEventoService _accesoEventoService;

        public TicketScannerController(IAccesoEventoService accesoEventoService)
        {
            _accesoEventoService = accesoEventoService;
        }

        [HttpPost("ValidarAccesoTicket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarAccesoTicket(TicketAccesoDto ticketAccesoDto)
        {
            var result = await _accesoEventoService.ValidarAccesoTicket(ticketAccesoDto);
            var response = new ApiResponse<AccesoEventoStatus>(result);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] AccesoEventoTicketQueryFilter filtros)
         {
            var ticketsAccesos = await _accesoEventoService.GetAccesosEventosTickets(filtros);
            var response = new ApiResponse<IEnumerable<AccesoEventoTicketDto>>(ticketsAccesos.Item1)
            {
                Meta = ticketsAccesos.Item2
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(ticketsAccesos.Item2));
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
