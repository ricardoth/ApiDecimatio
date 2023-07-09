using Decimatio.Domain.CustomEntities;
using Decimatio.Domain.QueryFilters;
using Newtonsoft.Json;

namespace Decimatio.WebApi.Controllers
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


    }
}