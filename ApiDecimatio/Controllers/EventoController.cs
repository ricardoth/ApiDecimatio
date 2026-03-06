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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _eventoService.GetAllEventos();
            if (result == null) return BadRequest();

            var response = new ApiResponse<IEnumerable<EventoDto>>(result);
            return Ok(response);
        }

        [HttpGet("GetEventosCombobox")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllEventosCombobox()
        {
            var result = await _eventoService.GetAllEventosCombobox();
            if (result == null) return BadRequest();

            var response = new ApiResponse<IEnumerable<EventoDto>>(result);
            return Ok(response);
        }


        [HttpGet("GetEventosPagination")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEventosPagination([FromQuery] EventoQueryFilter filtros)
        {
            var eventos = await _eventoService.GetAllEventosPaginated(filtros);

            var response = new ApiResponse<IEnumerable<EventoDto>>(eventos.Item1)
            {
                Meta = eventos.Item2
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(eventos.Item2));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _eventoService.GetById(id);
            var response = new ApiResponse<EventoDto>(result);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateEventoDto createEventoDto)
        {
            await _eventoService.AddEvento(createEventoDto);
            var response = new ApiResponse<CreateEventoDto>(createEventoDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, UpdateEventoDto eventoDto)
        { 
            if(id == 0) 
                return NotFound();

            eventoDto.IdEvento = id;
            var result = await _eventoService.UpdateEvento(eventoDto);
            var response = new ApiResponse<bool>(result);
            return Ok(response); 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var result = await _eventoService.DeleteEvento(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpGet("GetEventosFilter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetEventosFilter([FromQuery] string? filtro)
        {
            var result = await _eventoService.GetEventosFilter(filtro);
            var response = new ApiResponse<IEnumerable<EventoDto>>(result);
            return Ok(response);
        }
    }
}
