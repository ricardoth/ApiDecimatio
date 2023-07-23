namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _usuarioService.GetAllUsers();
            if (!result.Any()) return BadRequest();

            var response = new ApiResponse<IEnumerable<Usuario>>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _usuarioService.GetById(id);
            if (result == null)
                return BadRequest();

            var response = new ApiResponse<Usuario>(result);
            return Ok(response);
        }

        [HttpGet("GetUsersFilter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery]string? filtro)
        {
            var result = await _usuarioService.GetAllUsersFilter(filtro);
            //if (!result.Any()) return BadRequest();

            var response = new ApiResponse<IEnumerable<Usuario>>(result);
            return Ok(response);

        }
    }
}
