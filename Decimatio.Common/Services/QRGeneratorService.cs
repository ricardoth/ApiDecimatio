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


    }
}
