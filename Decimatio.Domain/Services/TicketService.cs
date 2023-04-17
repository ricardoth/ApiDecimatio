

namespace Decimatio.Domain.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;

        public TicketService(ITicketRepository ticketRepository, IQRGeneratorService qRGeneratorService)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
        }

        public async Task<string> AddTicket(Ticket ticket)
        {
            Bitmap qrCodeImage;
            try
            {
                using MemoryStream memoryStream = new MemoryStream();

                var result = await _ticketRepository.AddTicket(ticket);
                if (result != 0)
                {
                    qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticket);
                    qrCodeImage.Save(memoryStream, ImageFormat.Png);

                    byte[] imageBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);
                    //Generar insert TicketQR
                    TicketQR ticketQR = new TicketQR()
                    {
                        IdTicket = result,
                        Contenido= base64Image
                    }; 

                    await _ticketRepository.AddTicketQR(ticketQR);

                    File.WriteAllBytes("Ticket "+result.ToString()+".png", imageBytes);
                    //Guardar imagen en Blob Storage Azure
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

    }
}
