namespace Decimatio.Common.Interfaces
{
    public interface IBlobFilesService
    {
        Task AddTicketQRBlobStorage(byte[] imageBytes, string fileName);
        Task AddFlyerBlobStorage(byte[] imageBytes, string fileName);
        Task<string> GetImageFromBlobStorage(string imageNamePath);
    }
}
