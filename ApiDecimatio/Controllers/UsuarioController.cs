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
        public async Task<IActionResult> Get([FromQuery] UsuarioQueryFilter filtros)
        {
            var usuarios = await _usuarioService.GetAllUsers(filtros);

            var metaData = new MetaData
            {
                TotalCount = usuarios.TotalCount,
                PageSize = usuarios.PageSize,
                CurrentPage = usuarios.CurrentPage,
                TotalPages = usuarios.TotalPages,
                HasNextPage = usuarios.HasNextPage,
                HasPreviousPage = usuarios.HasPreviousPage,
                NextPageUrl = "",
                PreviousPageUrl = "",
            };

            var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuarios)
            {
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _usuarioService.GetById(id);
            if (result == null)
                return BadRequest("No se encuentra el elemento en la BD");

            var usuarioDto = _mapper.Map<UsuarioDto>(result);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }

        [HttpGet("GetUsersFilter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery]string? filtro)
        {
            var result = await _usuarioService.GetAllUsersFilter(filtro);
            if (result == null) return BadRequest("No se encontró ningún usuario");

            var usuariosDtos = _mapper.Map<IEnumerable<UsuarioDto>>(result);
            var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDtos);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            await _usuarioService.AddUsuario(createUsuarioDto);
            var response = new ApiResponse<CreateUsuarioDto>(createUsuarioDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            if (id <= 0)
                return NotFound("No se encuentra el elemento");

            updateUsuarioDto.IdUsuario = id;
            var result = await _usuarioService.UpdateUsuario(updateUsuarioDto);
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
                return NotFound("No se encuentra el elemento");

            var userBd = await _usuarioService.GetById(id);
            if (userBd == null)
                return BadRequest("El Usuario no existe en la BD");

            var result = await _usuarioService.DeleteUsuario(id); 
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto login)
        { 
            var result = await _usuarioService.Login(login);
            var usuarioDto = _mapper.Map<UsuarioDto>(result);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] UsuarioPassDto usuarioDto)
        {
            var usuario = _mapper.Map<UsuarioPass>(usuarioDto);
            var result = await _usuarioService.ChangePassword(usuario);
            var response = new ApiResponse<bool>(result);   
            return Ok(response);
        }
    }
}
