namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IValidator<Usuario> _validator;
        private readonly IPasswordService _passwordService;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, 
            IValidator<Usuario> validator, IPasswordService passwordService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _validator = validator;
            _passwordService = passwordService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] UsuarioQueryFilter filtros)
        {
            var usuarios = await _usuarioService.GetAllUsers(filtros);
            if (usuarios == null)
                return BadRequest("No se encontraron registros");

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

            var usuariosDtos = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDtos)
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
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto.Contrasena is not null) 
                usuarioDto.Contrasena = _passwordService.Hash(usuarioDto.Contrasena);

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            await _usuarioService.AddUsuario(usuario);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, UsuarioDto usuarioDto)
        {
            if (id <= 0)
                return NotFound("No se encuentra el elemento");

            if (usuarioDto.Contrasena is not null)
                usuarioDto.Contrasena = _passwordService.Hash(usuarioDto.Contrasena);

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.IdUsuario = id;

            var validationResult = _validator.Validate(usuario);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userBd = await _usuarioService.GetById(usuario.IdUsuario);
            if (userBd == null)
                return BadRequest("El Usuario no existe en la BD");

            var result = await _usuarioService.UpdateUsuario(usuario);
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
            var usuario = _mapper.Map<Usuario>(login);
            var result = await _usuarioService.Login(usuario);
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
