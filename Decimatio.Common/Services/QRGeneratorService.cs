namespace Decimatio.Common.Services
{
    public class QRGeneratorService : IQRGeneratorService
    {
        private readonly EncryptedTicketConfig _config;

        public QRGeneratorService(EncryptedTicketConfig config)
        {
            _config = config;        
        }

        public Bitmap GenerateQRCodeTicket<T>(T obj)
        { 
            string jsonString = JsonSerializer.Serialize(obj);
            string sha256Hash = EncryptTicketQR(jsonString);

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

        private string EncryptTicketQR(string jsonString)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                var sha256 = new SHA256Managed();
                byte[] keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_config.PrivateKey));
                byte[] ivBytes = new byte[16]; 
                Array.Copy(keyBytes, ivBytes, 16);

                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncryt = new())
                {
                    using (CryptoStream csEncrypt = new(msEncryt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new(csEncrypt))
                        {
                            streamWriter.Write(jsonString);
                        }
                        encrypted = msEncryt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
    }
}
