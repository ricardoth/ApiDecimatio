namespace Decimatio.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        private readonly ILugarService _lugarService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateLugarDto> _validator;

        public LugarController(ILugarService lugarService, IMapper mapper, IValidator<CreateLugarDto> validator)
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

            var response = new ApiResponse<IEnumerable<LugarDto>>(result);
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

            var response = new ApiResponse<LugarDto>(result);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateLugarDto createLugarDto)
        {
            await _lugarService.AddLugar(createLugarDto);
            var response = new ApiResponse<CreateLugarDto>(createLugarDto);
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
