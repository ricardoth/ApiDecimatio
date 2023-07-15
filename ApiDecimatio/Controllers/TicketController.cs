﻿namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IValidator<Ticket> _ticketValidator;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService, IValidator<Ticket> ticketValidator, IMapper mapper)
        {
            _ticketService = ticketService;
            _ticketValidator = ticketValidator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTickets([FromQuery] TicketQueryFilter filtros)
        {
            var tickets = await _ticketService.GetAllTickets(filtros);
            if (!tickets.Any())
                return BadRequest();

            var metaData = new MetaData
            {
                TotalCount = tickets.TotalCount,
                PageSize = tickets.PageSize,
                CurrentPage = tickets.CurrentPage,
                TotalPages = tickets.TotalPages,
                HasNextPage = tickets.HasNextPage,
                HasPreviousPage = tickets.HasPreviousPage,
                NextPageUrl = "",// _uriService.GetMenuPaginationUri(filtros, Url.RouteUrl(nameof(Get))).ToString(),
                PreviousPageUrl = "",//_uriService.GetMenuPaginationUri(filtros, Url.RouteUrl(nameof(Get))).ToString()
            };

            var response = new ApiResponse<IEnumerable<Ticket>>(tickets)
            {
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(response);
        }

        [HttpGet("GetTicketQR")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTicketQR([FromQuery] int idTicket)
        {
            var ticket = await _ticketService.GetTicketQR(idTicket);
            if (ticket == null)
                return BadRequest();

            var response = new ApiResponse<TicketQR>(ticket);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerarTicket([FromBody]TicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            var validationResult = _ticketValidator.Validate(ticket);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var item = await _ticketService.AddTicket(ticket);
            return Ok(item);
        }

        [HttpPost("GenerarTickets")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerarTickets([FromBody]IEnumerable<TicketDto> ticketsDto)
        {
            var tickets = _mapper.Map<IEnumerable<Ticket>>(ticketsDto);
            var result = await _ticketService.AddTickets(tickets);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{idTicket}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTicket(int idTicket)
        {
            try
            {
                if (idTicket == 0)
                    return NoContent();
                
                var result = await _ticketService.DeleteDownTicket(idTicket);

                if (!result)
                    return BadRequest();

                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}