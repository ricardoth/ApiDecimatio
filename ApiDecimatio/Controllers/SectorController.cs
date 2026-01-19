namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;
        private readonly IMapper _mapper;

        public SectorController(ISectorService sectorService, IMapper mapper)
        {
            _sectorService = sectorService;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _sectorService.GetAllSectores();
            if (!result.Any()) 
                return BadRequest();

            var sectoresDtos = _mapper.Map<IEnumerable<SectorDto>>(result);
            var response = new ApiResponse<IEnumerable<SectorDto>>(sectoresDtos);
            return Ok(response);
        }

        [HttpGet("GetSectoresByEvento/{idEvento}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSectoresByEvento(int idEvento)
        {
            var result = await _sectorService.GetSectoresByEvento(idEvento);
            if (result == null) 
                return BadRequest();

            var sectoresDto = _mapper.Map<IEnumerable<SectorDto>>(result);
            var response = new ApiResponse<IEnumerable<SectorDto>>(sectoresDto);
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

            var sectorDto = _mapper.Map<SectorDto>(result);
            var response = new ApiResponse<SectorDto>(sectorDto); 
            return Ok(response);        
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateSectorDto createSectorDto)
        {
            await _sectorService.AddSector(createSectorDto);
            var response = new ApiResponse<CreateSectorDto>(createSectorDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, UpdateSectorDto updateSectorDto) 
        {
            var result = await _sectorService.UpdateSector(updateSectorDto);
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
                return NotFound();

            var result = await _sectorService.DeleteSector(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
