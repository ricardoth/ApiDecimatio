namespace ApiDecimatio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketScannerController : ControllerBase
    {
        public TicketScannerController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string imagePath = "D://Richy/carnetreverso.jpeg";
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            var result = DecodeQRCode(imageBytes);

            if (result != null)
                return Ok(result.Text);
            else
                return NotFound("No se pudo leer el código QR");
        }

        private static Result DecodeQRCode(byte[] imageBytes)
        {
            using var memoryStream = new MemoryStream(imageBytes);
            using var image = (Bitmap)System.Drawing.Image.FromStream(memoryStream);

            if (image == null)
            {
                return null;
            }

            var barcodeReader = new BarcodeReaderGeneric
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new[] { BarcodeFormat.QR_CODE }
                }
            };

            return barcodeReader.Decode(image);
        }
    }
}
