namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IMercadoPagoService _mercadoPagoService;

        public MercadoPagoController(IMercadoPagoService mercadoPagoService)
        {
            _mercadoPagoService = mercadoPagoService;
        }

        [HttpPost("CrearPreferencia")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePreference([FromBody] PreferenceData data)
        {
            var result = await _mercadoPagoService.CrearSolicitudPago(data);
            return Ok(result);
        }

        [HttpGet("Feedback")]
        public IActionResult Feedback(string payment_id, string status, string merchant_order_id)
        {
            return Ok(new
            {
                Payment = payment_id,
                Status = status,
                MerchantOrder = merchant_order_id
            });
        }
    }
}