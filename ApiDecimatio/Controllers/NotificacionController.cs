using Decimatio.Domain.MercadoPagoEntitites;

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
                //llamar al endpoint de mercadopago GET payments, nos retorna external_preference y otros campos

                //Rescatar el campo external_preference (transactionId) y en estado approved (validar si efectuó el pago)

                //Rescatar tickets asociados al transactionId 
                //Guardar en BD la respuesta

                //Enviar a COLA Azure QUEUE
                //return 200 siempre
            }
            return Ok();
        }
    }
}
