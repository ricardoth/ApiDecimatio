namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComunaController : ControllerBase
    {
        private readonly IComunaService _comunaService;

        public ComunaController(IComunaService comunaService)
        {
            _comunaService = comunaService;
        }

        [HttpGet("{idRegion}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int idRegion)
        {
            var result = await _comunaService.GetComunasByRegion(idRegion);
            var response = new ApiResponse<IEnumerable<ComunaDto>>(result);
            return Ok(response);
        }
    }
}
