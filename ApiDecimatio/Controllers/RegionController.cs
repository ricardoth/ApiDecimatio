namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;   
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _regionService.GetAllRegions();
            if (result is null)
                return BadRequest("No se ha encontrado ningún registro");

            var regionesDtos = _mapper.Map<IEnumerable<RegionDto>>(result);
            var response = new ApiResponse<IEnumerable<RegionDto>>(regionesDtos);
            return Ok(response);
        }
    }
}
