using Azure.Storage.Blobs.Models;

namespace Decimatio.Common.Services
{
    public class BlobFilesService : IBlobFilesService
    {
        private readonly BlobContainerConfig _containerConfig;
        private readonly BlobServiceClient _blobServiceClient;


        public BlobFilesService(BlobContainerConfig containerConfig)
        {
            _containerConfig = containerConfig;
            _blobServiceClient = new BlobServiceClient(_containerConfig.ConnectionString);
        }

        public async Task AddTicketQRBlobStorage(byte[] imageBytes, string fileName)
        {
            
            var memoryStream = new MemoryStream(imageBytes);

            try
            {
                string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var blobServiceClient = new BlobServiceClient(_containerConfig.ConnectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerConfig.ContainerName);
                string filePathName = $"{_containerConfig.FolderName}{timeStamp}_{fileName}";
                var blobClient = blobContainerClient.GetBlobClient(filePathName);
                var result = await blobClient.UploadAsync(memoryStream, overwrite: true);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo agregar el ticketQR al Azure Blob Storage", ex);
            }
            finally
            {
                memoryStream.Close();
            }
        }

        public async Task<string> GetImageFromBlobStorage(string imageName, string folderName)
        {
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerConfig.ContainerName);
                var filePathName = $"{folderName}{imageName}";
                var blobClient = containerClient.GetBlobClient(filePathName);

                if (!await blobClient.ExistsAsync())
                    throw new Exception($"La imagen no existe en el contenedor {imageName}");

                BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();

                await downloadInfo.Content.CopyToAsync(memoryStream);

                byte[] imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener la imagen desde Azure Blob Storage", ex);
            }
            finally
            {
                memoryStream.Close();
            }

        }
        
    }
}
