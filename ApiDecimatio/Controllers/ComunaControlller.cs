namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComunaController : ControllerBase
    {
        private readonly IComunaService _comunaService;
        private readonly IMapper _mapper;

        public ComunaController(IComunaService comunaService, IMapper mapper)
        {
            _comunaService = comunaService;
            _mapper = mapper;
        }

        [HttpGet("{idRegion}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int idRegion)
        {
            var result = await _comunaService.GetComunasByRegion(idRegion);
            if (!result.Any())
                return NotFound("No se encuentran comunas con la región indicada");

            var comunasDtos = _mapper.Map<IEnumerable<ComunaDto>>(result);
            var response = new ApiResponse<IEnumerable<ComunaDto>>(comunasDtos);
            return Ok(response);
        }
    }
}
