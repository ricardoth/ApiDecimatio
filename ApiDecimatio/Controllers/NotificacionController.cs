using Decimatio.Domain.MercadoPagoEntitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly IMapper _mapper;

        public NotificacionController(IMercadoPagoService mercadoPagoService, IMapper mapper)
        {
            _mercadoPagoService = mercadoPagoService;
            _mapper = mapper;
        }

        [HttpPost("PaymentNotification")]
        public async Task<IActionResult> PaymentNotification([FromBody] MercadoPagoNotification notification)
        {
            if (notification.Type == "payment")
            {
                var paymentId = notification.Data.Id;
                //var payment = 
                //Guardar en  bd la notificación
                var notificationResult = await _mercadoPagoService.CrearNotificacionPago(notification);
                //Guardar en BD la traza del pago
            }
            return Ok();
        }
    }
}
