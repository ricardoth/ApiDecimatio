namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;
        private readonly IMapper _mapper;
        private readonly IValidator<Sector> _validator;

        public SectorController(ISectorService sectorService, IMapper mapper, IValidator<Sector> validator)
        {
            _sectorService = sectorService;
            _mapper = mapper;
            _validator = validator;

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
            if (result == null) 
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] SectorDto sectorDto)
        {
            var sector = _mapper.Map<Sector>(sectorDto);
            var validationResult = _validator.Validate(sector);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _sectorService.AddSector(sector);

            var response = new ApiResponse<SectorDto>(sectorDto);
            return Ok(response);
        }
    }
}
