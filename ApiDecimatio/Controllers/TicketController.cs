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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
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
    }
}