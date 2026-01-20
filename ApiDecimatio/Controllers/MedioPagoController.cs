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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _medioPagoService.GetMediosPagosAsync();
            var response = new ApiResponse<IEnumerable<MedioPagoDto>>(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _medioPagoService.GetMedioPagoAsync(id);
            var response = new ApiResponse<MedioPagoDto>(result);
            return Ok(response);  
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] MedioPagoDto medioPagoDto)
        {
            await _medioPagoService.AddMedioPagoAsync(medioPagoDto);
            var response = new ApiResponse<MedioPagoDto>(medioPagoDto);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, MedioPagoDto medioPagoDto)
        {
            medioPagoDto.IdMedioPago = id;
            var result = await _medioPagoService.UpdateMedioPagoAsync(medioPagoDto);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medioPagoService.DeleteMedioPagoAsync(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
