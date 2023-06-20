namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _eventoService.GetAllEventos();
            if (!result.Any()) return BadRequest();
            var response = new ApiResponse<IEnumerable<Evento>>(result);
            return Ok(response);
        }
    }
}
