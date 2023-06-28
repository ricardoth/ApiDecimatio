namespace Decimatio.WebApi.Controllers
{
    [Authorize]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _sectorService.GetAllSectores();
            if (!result.Any()) 
                return BadRequest();
            var response = new ApiResponse<IEnumerable<Sector>>(result);
            return Ok(response);
        }

        [HttpGet("GetSectoresByEvento/{idEvento}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSectoresByEvento(int idEvento)
        {
            var result = await _sectorService.GetSectoresByEvento(idEvento);
            if (!result.Any()) 
                return BadRequest();
            var response = new ApiResponse<IEnumerable<Sector>>(result);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _sectorService.GetById(id);  
            if (result == null)
                return BadRequest();
            var response = new ApiResponse<Sector>(result); 
            return Ok(response);        
        } 
    }
}
