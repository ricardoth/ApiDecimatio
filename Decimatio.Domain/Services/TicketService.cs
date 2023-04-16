using Decimatio.Common.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

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

        public async Task<int> AddTicket(Ticket ticket)
        {
            Bitmap qrCodeImage;
            try
            {
                var result = await _ticketRepository.AddTicket(ticket);
                if (result == 1)
                {
                    qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticket);
                    qrCodeImage.Save("MiEntradaConcierto", ImageFormat.Png);
                    return result;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
