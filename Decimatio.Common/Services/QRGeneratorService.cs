using Decimatio.Domain.ValueObjects;

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
