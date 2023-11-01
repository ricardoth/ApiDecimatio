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

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, IValidator<Usuario> validator)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _validator = validator;
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

            var response = new ApiResponse<IEnumerable<Usuario>>(usuarios)
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
            if (result == null) return BadRequest("No se encontró ningún usuario");

            var response = new ApiResponse<IEnumerable<Usuario>>(result);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuarioDto)
        { 
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var validationResult = _validator.Validate(usuario);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

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

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.IdUsuario = id;

            var validationResult = _validator.Validate(usuario);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

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
            var result = await _usuarioService.DeleteUsuario(id); 
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
