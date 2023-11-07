namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedioPagoController : ControllerBase
    {
        private readonly IMedioPagoService _medioPagoService;
        private readonly IMapper _mapper;

        public MedioPagoController(IMedioPagoService medioPagoService, IMapper mapper)
        {
            _medioPagoService = medioPagoService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _medioPagoService.GetMediosPagosAsync();
            if (result == null) 
                return BadRequest();

            var medioPagoDtos = _mapper.Map<IEnumerable<MedioPagoDto>>(result);
            var response = new ApiResponse<IEnumerable<MedioPagoDto>>(medioPagoDtos);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _medioPagoService.GetMedioPagoAsync(id);
            if (result == null)
                return BadRequest();

            var medioPagoDto = _mapper.Map<MedioPagoDto>(result);
            var response = new ApiResponse<MedioPagoDto>(medioPagoDto);
            return Ok(response);  
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedioPago medioPago)
        {
            await _medioPagoService.AddMedioPagoAsync(medioPago);
            return Ok(medioPago);
        }

    }
}
