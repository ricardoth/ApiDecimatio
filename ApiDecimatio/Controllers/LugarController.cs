namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        private readonly ILugarService _lugarService;
        private readonly IMapper _mapper;
        private readonly IValidator<Lugar> _validator;

        public LugarController(ILugarService lugarService, IMapper mapper, IValidator<Lugar> validator)
        {
            _lugarService = lugarService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        { 
            var result = await _lugarService.GetAllLugares();
            if (result == null)
                return BadRequest();

            var lugaresDtos = _mapper.Map<IEnumerable<LugarDto>>(result);
            var response = new ApiResponse<IEnumerable<LugarDto>>(lugaresDtos);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _lugarService.GetById(id);
            if (result == null)
                return BadRequest();

            var lugarDto = _mapper.Map<LugarDto>(result);
            var response = new ApiResponse<LugarDto>(lugarDto);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] LugarDto lugarDto)
        {
            var lugar = _mapper.Map<Lugar>(lugarDto);
            var validationResult = _validator.Validate(lugar);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _lugarService.AddLugar(lugar);

            var response = new ApiResponse<LugarDto>(lugarDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, UpdateLugarDto updateLugarDto)
        {
            if (id <= 0)
                return NotFound();

            updateLugarDto.IdLugar = id;
            var result = await _lugarService.UpdateLugar(updateLugarDto);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            var result = await _lugarService.DeleteLugar(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
