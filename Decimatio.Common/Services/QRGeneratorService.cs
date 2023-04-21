using PuppeteerSharp;

namespace Decimatio.Common.Services
{
    public class QRGeneratorService : IQRGeneratorService
    {
        public Bitmap GenerateQRCodeTicket<T>(T obj)
        { 
            string jsonString = JsonSerializer.Serialize(obj);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            
            Bitmap qrCodeImage = new Bitmap(new MemoryStream(qrCode.GetGraphic(10)));
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

    }
}
