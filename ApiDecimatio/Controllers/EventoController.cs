using Decimatio.Domain.Interfaces;

namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IValidator<Evento> _validator;
        private readonly IMapper _mapper;

        public EventoController(IEventoService eventoService, IValidator<Evento> validator, IMapper mapper)
        {
            _eventoService = eventoService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _eventoService.GetAllEventos();
            if (result == null) return BadRequest();

            var eventosDtos = _mapper.Map<IEnumerable<EventoDto>>(result);
            var response = new ApiResponse<IEnumerable<EventoDto>>(eventosDtos);
            return Ok(response);
        }

        [HttpGet("GetEventosPagination")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEventosPagination([FromQuery] EventoQueryFilter filtros)
        {
            var eventos = await _eventoService.GetAllEventosPaginated(filtros);

            var metaData = new MetaData
            {
                TotalCount = eventos.TotalCount,
                PageSize = eventos.PageSize,
                CurrentPage = eventos.CurrentPage,
                TotalPages = eventos.TotalPages,
                HasNextPage = eventos.HasNextPage,
                HasPreviousPage = eventos.HasPreviousPage,
                NextPageUrl = "",
                PreviousPageUrl = "",
            };

            var eventosDtos = _mapper.Map<IEnumerable<EventoDto>>(eventos);
            var response = new ApiResponse<IEnumerable<EventoDto>>(eventosDtos)
            {
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return NotFound();

            var result = await _eventoService.GetById(id);
            if (result == null)
                return BadRequest();

            var eventoDto = _mapper.Map<EventoDto>(result);
            var response = new ApiResponse<EventoDto>(eventoDto);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] EventoPostDto eventoDto)
        {
            var evento = _mapper.Map<Evento>(eventoDto);
            var validationResult = _validator.Validate(evento);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _eventoService.AddEvento(evento);

            var response = new ApiResponse<EventoPostDto>(eventoDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, EventoPostDto eventoDto)
        { 
            if(id == 0) 
                return NotFound();

            var evento = _mapper.Map<Evento>(eventoDto);
            evento.IdEvento = id;

            var validationResult = _validator.Validate(evento);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _eventoService.UpdateEvento(evento);
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
        public async Task<IActionResult> Get([FromQuery] string? filtro)
        {
            var result = await _eventoService.GetEventosFilter(filtro);
            var eventosDtos = _mapper.Map<IEnumerable<EventoDto>>(result);
            var response = new ApiResponse<IEnumerable<EventoDto>>(eventosDtos);
            return Ok(response);
        }
    }
}
