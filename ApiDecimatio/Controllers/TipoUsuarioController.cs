namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoUsuarioService _tipoUsuarioService;

        public TipoUsuarioController(IMapper mapper, ITipoUsuarioService tipoUsuarioService)
        {
            _mapper = mapper;
            _tipoUsuarioService = tipoUsuarioService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _tipoUsuarioService.GetAllTiposUsuarios();
            if (result == null)
                return BadRequest("No se encontraron elementos");
            var tipoUsuarioDto = _mapper.Map<IEnumerable<TipoUsuarioDto>>(result);
            var response = new ApiResponse<IEnumerable<TipoUsuarioDto>>(tipoUsuarioDto);
            return Ok(response);    
        }
    }
}
