using Decimatio.Domain.Entities;
using Decimatio.Domain.Interfaces;
using System.Net;

namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerarTicket([FromBody]Ticket ticket)
        {
            var item = await _ticketService.AddTicket(ticket);
            return Ok(item);
        }
    }
}
