namespace Decimatio.Common.Interfaces
{
    public interface IQRGeneratorService
    {
        Bitmap GenerateQRCodeTicket<T>(T obj);
    }
}
