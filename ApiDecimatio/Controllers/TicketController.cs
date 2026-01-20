namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTickets([FromQuery] TicketQueryFilter filtros)
        {
            var tickets = await _ticketService.GetAllTickets(filtros);

            var response = new ApiResponse<IEnumerable<TicketDto>>(tickets.Item1)
            {
                Meta = tickets.Item2
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(tickets.Item2));
            return Ok(response);
        }

        [HttpGet("GetTicketQR")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTicketQR([FromQuery] int idTicket)
        {
            var ticket = await _ticketService.GetTicketQR(idTicket);
            var response = new ApiResponse<TicketQRDto>(ticket);
            return Ok(response);
        }

        [HttpGet("GetTicketVoucherPDF")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTicketVoucherPDF([FromQuery] int idTicket)
        {
            if (idTicket == 0)
                return NotFound();

            var ticket = await _ticketService.GetTicketVoucherPDF(idTicket);
            var response = new ApiResponse<TicketQRDto>(ticket);
            return Ok(response);

        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerarTicket([FromBody]TicketDto ticketDto)
        {
            var item = await _ticketService.AddTicket(ticketDto);
            return Ok(item);
        }

        [HttpPost("GenerarTickets")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerarTickets([FromBody]IEnumerable<TicketDto> ticketsDto)
        {
            var result = await _ticketService.AddTickets(ticketsDto);
            return Ok(result);
        }

        [HttpPost("TicketQueue")]
        public async Task<IActionResult> GenerarTicketQueue([FromBody] PreferenceRequest preferenceRequest)
        {
            var result = await _ticketService.AddQueueTicket(preferenceRequest.PreferenceCode);
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTicket([FromQuery]int idTicket, bool activo)
        {
            if (idTicket == 0)
                return NoContent();
                
            var result = await _ticketService.DeleteDownTicket(idTicket, activo);

            if (!result)
                return BadRequest();

            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpGet("GetPreferenceTickets/{transactionId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPreferenceTickets(string transactionId)
        {
            var result = await _ticketService.GetPreferenceTicketsByTransaction(transactionId);
            var response = new ApiResponse<IEnumerable<PreferenceTicketDto>>(result);
            return Ok(response);
        }

        #region Excel
        [HttpGet("GetTicketsExcel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTicketsExcel([FromQuery] TicketQueryFilter filtros)
        {
            var tickets = await _ticketService.GetAllTicketsExcel(filtros);
            var response = new ApiResponse<IEnumerable<TicketDto>>(tickets);
            return Ok(response);
        }
        #endregion

        #region Preference Tickets
        [HttpGet("GetPreferenceTickets")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ticketService.GetAllPreferenceTickets();
            var response = new ApiResponse<IEnumerable<PreferenceTicketDto>>(result);
            return Ok(response);
        }
        #endregion
    }
}