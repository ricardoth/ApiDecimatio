namespace Decimatio.Common.Services
{
    public class QRGeneratorService : IQRGeneratorService
    {
        public Bitmap GenerateQRCodeTicket<T>(T obj)
        { 
            string jsonString = JsonSerializer.Serialize(obj);
            //string jsonString = JsonConvert.SerializeObject(obj);
            string sha256Hash = GenerateSha256Hash(jsonString);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(sha256Hash, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            
            Bitmap qrCodeImage = new Bitmap(new MemoryStream(qrCode.GetGraphic(20)));
            return qrCodeImage;
        }

        public async Task<Bitmap> RenderHtmlToBitmapAsync(string htmlContent)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            var screenshotStream = await page.ScreenshotStreamAsync(new ScreenshotOptions { Type = ScreenshotType.Png });
            Bitmap bitmap = new Bitmap(screenshotStream);

            await browser.CloseAsync();
            return bitmap;
        }

        public async Task<byte[]> RenderHtmlToPdfAsync(string htmlContent)
        {
            using var memoryStram = new MemoryStream();
            try
            {
                var renderer = new HtmlToPdf();
                var pdf = renderer.RenderHtmlAsPdf(htmlContent);
               
                pdf.Stream.CopyTo(memoryStram);
                return memoryStram.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el PDF: {ex.Message}", ex);
            }
            finally { memoryStram.Close(); }
        }

        public async Task<string> MergePdfFiles(List<string> strList)
        {
            var outputStream = new MemoryStream();
            try
            {
                var pdfList = new List<PdfDocument>();
                foreach (var item in strList)
                {
                    byte[] pdfByte = Convert.FromBase64String(item);
                    var pdf = new PdfDocument(new MemoryStream(pdfByte));
                    pdfList.Add(pdf);
                }
                PdfDocument mergedPdf = PdfDocument.Merge(pdfList);

                mergedPdf.Stream.CopyTo(outputStream);
                var ms = outputStream.ToArray();
                return Convert.ToBase64String(ms);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el PDF: {ex.Message}", ex);
            }
            finally { outputStream.Close(); }
        }

        private string GenerateSha256Hash(string input)
        {
            using var sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
