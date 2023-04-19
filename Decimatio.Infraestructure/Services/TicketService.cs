namespace Decimatio.Infraestructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IBlobFilesService _blobFilesService;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IQRGeneratorService qRGeneratorService, 
            IBlobFilesService blobFilesService, BlobContainerConfig containerConfig,
            IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
        }

        public async Task<string> AddTicket(Ticket ticket)
        {
            Bitmap qrCodeImage;
            try
            {
                using MemoryStream memoryStream = new();

                var result = await _ticketRepository.AddTicket(ticket);
                if (result != 0)
                {
                    Ticket ticketWithInfo = await _ticketRepository.GetInfoTicket(result);
                    var ticketDto = _mapper.Map<Ticket>(ticketWithInfo);

                    qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticketDto);
                    qrCodeImage.Save(memoryStream, ImageFormat.Png);

                    byte[] imageBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);
                    string fileName = $"Ticket N° {ticketDto.IdTicket} | Rut: {ticketDto.Usuario.Rut}-{ticketDto.Usuario.DV}.png";

                    TicketQR ticketQR = new TicketQR()
                    {
                        IdTicket = result,
                        Contenido= base64Image
                    };

                    var ticketQRResponse = await AddTicketQR(ticketQR);

                    //Guardar imagen en Blob Storage Azure
                    await _blobFilesService.AddTicketQRBlobStorage(imageBytes, fileName);
                    return base64Image;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TicketQR> AddTicketQR(TicketQR ticketQR)
        {
            try
            {
                await _ticketRepository.AddTicketQR(ticketQR);
                return ticketQR;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //private async Task<int> AddTicketQRBlobStorage(byte[] imageBytes)
        //{
        //    var blobServiceClient = new BlobServiceClient(_containerConfig.ConnectionString);

        //    var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerConfig.ContainerName);
        //    var blobClient = blobContainerClient.GetBlobClient("ticketQR.png");
        //    using var memoryStream = new MemoryStream(imageBytes);
        //    var result = await blobClient.UploadAsync(memoryStream, overwrite: true);
        //    return 1;

        //}
    }
}
