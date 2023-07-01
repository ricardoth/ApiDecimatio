namespace Decimatio.Common.Interfaces
{
    public interface IQRGeneratorService
    {
        Bitmap GenerateQRCodeTicket<T>(T obj);
        Task<Bitmap> RenderHtmlToBitmapAsync(string htmlContent);
        Task<byte[]> RenderHtmlToPdfAsync(string htmlContent);
        Task<string> MergePdfFiles(List<string> strList);
    }
}
