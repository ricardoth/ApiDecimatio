using Decimatio.Domain.MercadoPagoEntities;

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

        [HttpGet("GetCliente")]
        public async Task<IActionResult> GetCliente()
        {
            var client = await _mercadoPagoService.SearchCliente();
            return Ok(client);  
        }

        [HttpGet("GetTarjetas")]
        public async Task<IActionResult> GetTarjetas()
        {
            var client = await _mercadoPagoService.GetTarjetasCliente();
            return Ok(client);
        }

        [HttpPost("CreateCliente")]
        public async Task<IActionResult> CreateCliente()
        {
            var preference = await _mercadoPagoService.CrearClientePago();
            return Ok(preference);
        }

        [HttpPost("CrearPreferencia")]
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