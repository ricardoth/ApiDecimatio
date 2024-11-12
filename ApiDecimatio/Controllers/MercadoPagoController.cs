namespace Decimatio.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        //private readonly IMercadoPagoService _mercadoPagoService;
        //private readonly IMapper _mapper;

        //public MercadoPagoController(IMercadoPagoService mercadoPagoService, IMapper mapper)
        //{
        //    _mercadoPagoService = mercadoPagoService;
        //    _mapper = mapper;

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _mercadoPagoService.GetAllPreferenceTickets();
        //    var preferenceTicketsDtos = _mapper.Map<IEnumerable<PreferenceTicketDto>>(result);
        //    var response = new ApiResponse<IEnumerable<PreferenceTicketDto>>(preferenceTicketsDtos);
        //    return Ok(response);
        //}
        

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