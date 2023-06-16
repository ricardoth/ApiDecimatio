namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;

        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _sectorService.GetAllSectores();
            if (!result.Any()) return BadRequest();
            return Ok(result);
        }
    }
}
