//using CoreHtmlToImage;
//using PuppeteerSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            
            Bitmap qrCodeImage = new Bitmap(new MemoryStream(qrCode.GetGraphic(20)));
            return qrCodeImage;
        }

        public async Task<Bitmap> RenderHtmlToBitmapAsync(string htmlContent)
        {
            //await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            //using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            //using var page = await browser.NewPageAsync();
            //await page.SetContentAsync(htmlContent);

            //var screenshotStream = await page.ScreenshotStreamAsync(new ScreenshotOptions { Type = ScreenshotType.Png });
            //Bitmap bitmap = new Bitmap(screenshotStream);

            //await browser.CloseAsync();
            //return bitmap;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--window-size=1280,1024");
            using var driver = new ChromeDriver(chromeOptions);

            string htmlDataUrl = "data:text/html;charset=utf-8;base64," + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(htmlContent));

            // Navega a la URL codificada en base64 con el contenido HTML
            driver.Navigate().GoToUrl(htmlDataUrl);

            // Captura una imagen de la página web y guarda la imagen en un archivo PNG
            Screenshot screenshot = driver.GetScreenshot();
            //string outputPath = "output.png";
            //screenshot.SaveAsFile(outputPath, ScreenshotImageFormat.Png);

            using MemoryStream memoryStream = new MemoryStream(screenshot.AsByteArray);
            Bitmap bitmap = new Bitmap(memoryStream);
            return bitmap;
        }

    }
}
