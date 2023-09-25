namespace ApiDecimatio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketScannerController : ControllerBase
    {
        private readonly IAccesoEventoService _accesoEventoService;
        private readonly IMapper _mapper;

        public TicketScannerController(IAccesoEventoService accesoEventoService, IMapper mapper)
        {
            _accesoEventoService = accesoEventoService;
            _mapper = mapper;
        }

        [HttpPost("ValidarAccesoTicket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarAccesoTicket(TicketAccesoDto ticketAccesoDto)
        {
            var ticketAcceso = _mapper.Map<TicketAcceso>(ticketAccesoDto);
            var result = await _accesoEventoService.ValidarAccesoTicket(ticketAcceso);
            var response = new ApiResponse<AccesoEventoStatus>(result);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] AccesoEventoTicketQueryFilter filtros)
         {
            var ticketsAccesos = await _accesoEventoService.GetAccesosEventosTickets(filtros);
            if (ticketsAccesos == null)
                return BadRequest();

            var metaData = new MetaData
            {
                TotalCount = ticketsAccesos.TotalCount,
                PageSize = ticketsAccesos.PageSize,
                CurrentPage = ticketsAccesos.CurrentPage,
                TotalPages = ticketsAccesos.TotalPages,
                HasNextPage = ticketsAccesos.HasNextPage,
                HasPreviousPage = ticketsAccesos.HasPreviousPage,
                NextPageUrl = "",
                PreviousPageUrl = "",
            };

            var ticketsAccesoDto = _mapper.Map<IEnumerable<AccesoEventoTicketDto>>(ticketsAccesos);
            var response = new ApiResponse<IEnumerable<AccesoEventoTicketDto>>(ticketsAccesoDto)
            {
                Meta = metaData
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(response);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    string imagePath = "D://Richy/carnetreverso.jpeg";
        //    byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        //    var result = DecodeQRCode(imageBytes);

        //    if (result != null)
        //        return Ok(result.Text);
        //    else
        //        return NotFound("No se pudo leer el código QR");
        //}

        //private static Result DecodeQRCode(byte[] imageBytes)
        //{
        //    using var memoryStream = new MemoryStream(imageBytes);
        //    using var image = (Bitmap)System.Drawing.Image.FromStream(memoryStream);

        //    if (image == null)
        //    {
        //        return null;
        //    }

        //    var barcodeReader = new BarcodeReaderGeneric
        //    {
        //        AutoRotate = true,
        //        TryInverted = true,
        //        Options = new DecodingOptions
        //        {
        //            TryHarder = true,
        //            PossibleFormats = new[] { BarcodeFormat.QR_CODE }
        //        }
        //    };

        //    return barcodeReader.Decode(image);
        //}
    }
}
