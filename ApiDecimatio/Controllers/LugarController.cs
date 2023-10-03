namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        private readonly ILugarService _lugarService;
        private readonly IMapper _mapper;

        public LugarController(ILugarService lugarService, IMapper mapper)
        {
            _lugarService = lugarService;
            _mapper = mapper;
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
    }
}
