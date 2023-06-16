namespace Decimatio.Infraestructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IBlobFilesService _blobFilesService;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IQRGeneratorService qRGeneratorService, 
            IBlobFilesService blobFilesService, IMapper mapper )
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
        }

        public async Task<string> AddTicket(Ticket ticket)
        {
            try
            {
                var result = await _ticketRepository.AddTicket(ticket);

                if (result == 0)
                    return null;

                Ticket ticketWithInfo = await _ticketRepository.GetInfoTicket(result);
                var ticketDto = _mapper.Map<TicketBodyQRDto>(ticketWithInfo);

                var json = new TicketInfoDto()
                {
                    IdTicket = result,
                    FechaTicket = ticketDto.FechaTicket,
                    MontoTotal = ticketDto.MontoTotal,
                    RutUsuario = $"{ticketDto.Usuario.Rut}-{ticketDto.Usuario.DV}",
                    Nombres = ticketDto.Usuario.Nombres,
                    ApellidoP = ticketDto.Usuario.ApellidoP,
                    ApellidoM = ticketDto.Usuario.ApellidoM,
                    Correo = ticketDto.Usuario.Correo
                };

                string base64QRImage = GeneratoQRCodeBase64(json);

                TicketQR ticketQR = new TicketQR()
                {
                    IdTicket = result,
                    Contenido = base64QRImage,
                    Ticket = ticket,
                };

                await AddTicketQR(ticketQR);

                string fileName = GetTicketFileName(ticketDto);
                string base64HtmlTicket = await SaveTicketImageToBlobStorage(base64QRImage, ticketDto, fileName);

                return base64HtmlTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al Crear el Ticket", ex);
            }
        }

        private string GeneratoQRCodeBase64(TicketInfoDto ticket)
        {
            using MemoryStream memoryStream = new();
            Bitmap qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticket);
            qrCodeImage.Save(memoryStream, ImageFormat.Png);
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

        private string GetTicketFileName(TicketBodyQRDto ticketDto)
        {
            return $"Ticket N° {ticketDto.IdTicket} | Rut: {ticketDto.Usuario.Rut}-{ticketDto.Usuario.DV}.pdf";
        }

        private async Task<string> SaveTicketImageToBlobStorage(string base64QRImage, TicketBodyQRDto ticketDto, string fileName)
        {
            var ticketResultImage = await EscribirPlantilla(base64QRImage, ticketDto);
            await _blobFilesService.AddTicketQRBlobStorage(ticketResultImage, fileName);
            return Convert.ToBase64String(ticketResultImage);
        }

        public async Task<TicketQR> AddTicketQR(TicketQR ticketQR)
        {
            if (ticketQR == null)
                throw new ArgumentNullException(nameof(ticketQR));

            try
            {
                await _ticketRepository.AddTicketQR(ticketQR);
                return ticketQR;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al agregar el TicketQR", ex);
            }
        }

        public async Task<byte[]> EscribirPlantilla(string base64Image, TicketBodyQRDto ticket)
        {
            string currentDirectory = Directory.GetCurrentDirectory() + "\\Template";
            string htmlTemplatePath = Path.Combine(currentDirectory, "IaETicket.html");
            string htmlTemplate = File.ReadAllText(htmlTemplatePath);
            string logoImage = Path.Combine(currentDirectory, "decimatio2.jpg");

            byte[] logoBytes = File.ReadAllBytes(logoImage);
            string logoBase64Image = Convert.ToBase64String(logoBytes);

            string formatDay = ticket.Evento.Fecha.ToString("dddd", new CultureInfo("es-ES"));
            string anio = ticket.Evento.Fecha.ToString("yyyy", new CultureInfo("es-ES"));
            string formatDate = ticket.Evento.Fecha.ToString("d' de 'MMMM", new CultureInfo("es-ES"));
            string formatHora = ticket.Evento.Fecha.ToString("HH:mm");
            long montoTotalFormat = (long)ticket.MontoTotal;
            string pais = "Chile";
            string comuna = ticket.Sector.Lugar.Comuna.NombreComuna;
          

            string htmlWithImage = htmlTemplate.Replace("{Base64Image}", base64Image)
                                    .Replace("{LogoImage}", logoBase64Image)
                                    .Replace("{DiaEvento}", formatDay.ToUpper())
                                    .Replace("{FechaEvento}", formatDate)
                                    .Replace("{AnioEvento}", anio)
                                    .Replace("{NombreEvento}", ticket.Evento.NombreEvento)
                                    .Replace("{NombreSector}", ticket.Sector.NombreSector)
                                    .Replace("{MontoTotal}", montoTotalFormat.ToString())
                                    .Replace("{Pais}", pais)
                                    .Replace("{Comuna}", comuna)
                                    .Replace("{Direccion}", ticket.Sector.Lugar.Ubicacion)
                                    .Replace("{Numeracion}", ticket.Sector.Lugar.Numeracion)
                                    .Replace("{HoraEvento}", formatHora)
                                    .Replace("{NombreLugar}", ticket.Sector.Lugar.NombreLugar)
                                    .Replace("{NombreEventoUser}", ticket.Evento.NombreEvento)
                                    .Replace("{Titulo}", ticket.Evento.NombreEvento)
                                    .Replace("{NombreSectorUser}", ticket.Sector.NombreSector)
                                    .Replace("{HoraEventoUser}", formatHora)
                                    .Replace("{IdTicket}", ticket.IdTicket.ToString())
                                    .Replace("{IdTicketUser}", ticket.IdTicket.ToString());
            var htmlPdf = await _qrGeneratorService.RenderHtmlToPdfAsync(htmlWithImage);

            return htmlPdf;

        }
    }
}
