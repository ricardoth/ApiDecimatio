

namespace Decimatio.Common.Services
{
    public class BlobFilesService : IBlobFilesService
    {
        private readonly BlobContainerConfig _containerConfig;

        public BlobFilesService(BlobContainerConfig containerConfig)
        {
            _containerConfig = containerConfig;
        }

        public async Task AddTicketQRBlobStorage(byte[] imageBytes, string fileName)
        {
            string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var blobServiceClient = new BlobServiceClient(_containerConfig.ConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerConfig.ContainerName);
            string filePathName = $"{_containerConfig.FolderName}{timeStamp}_{fileName}"; 
            var blobClient = blobContainerClient.GetBlobClient(filePathName);
            using var memoryStream = new MemoryStream(imageBytes);
            var result = await blobClient.UploadAsync(memoryStream, overwrite: true);
        }
    }
}
