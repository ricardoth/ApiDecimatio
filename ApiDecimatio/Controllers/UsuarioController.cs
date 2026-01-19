namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<IEnumerable<UsuarioDto>>> Get([FromQuery] UsuarioQueryFilter filtros)
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
            return response;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<UsuarioDto>> Get(int id)
        {
            var result = await _usuarioService.GetById(id);
            var response = new ApiResponse<UsuarioDto>(result);
            return response;
        }

        [HttpGet("GetUsersFilter")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ApiResponse<IEnumerable<UsuarioDto>>> Get([FromQuery]string? filtro)
        {
            var result = await _usuarioService.GetAllUsersFilter(filtro);
            var response = new ApiResponse<IEnumerable<UsuarioDto>>(result);
            return response;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<CreateUsuarioDto>> Post([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            await _usuarioService.AddUsuario(createUsuarioDto);
            var response = new ApiResponse<CreateUsuarioDto>(createUsuarioDto);
            return response;
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ApiResponse<bool>> Put(int id, UpdateUsuarioDto updateUsuarioDto)
        {
            updateUsuarioDto.IdUsuario = id;
            var result = await _usuarioService.UpdateUsuario(updateUsuarioDto);
            var response = new ApiResponse<bool>(result);
            return response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
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
        public async Task<ApiResponse<UsuarioDto>> Login([FromBody] UsuarioLoginDto login)
        { 
            var result = await _usuarioService.Login(login);
            var response = new ApiResponse<UsuarioDto>(result);
            return response;
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<bool>> ChangePassword([FromBody] UsuarioPassDto usuarioDto)
        {
            var result = await _usuarioService.ChangePassword(usuarioDto);
            return new ApiResponse<bool>(result);   
        }
    }
}
