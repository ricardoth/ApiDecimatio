namespace Decimatio.WebApi.Controllers
{
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
            return Ok(result);
        }
    }
}
