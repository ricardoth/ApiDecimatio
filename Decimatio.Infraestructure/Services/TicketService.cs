namespace Decimatio.Infraestructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IBlobFilesService _blobFilesService;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnviroment;

        public TicketService(ITicketRepository ticketRepository, IQRGeneratorService qRGeneratorService, 
            IBlobFilesService blobFilesService, BlobContainerConfig containerConfig,
            IMapper mapper, IHostEnvironment hostEnvironment)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
            _hostEnviroment = hostEnvironment;
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
            return $"Ticket N° {ticketDto.IdTicket} | Rut: {ticketDto.Usuario.Rut}-{ticketDto.Usuario.DV}.png";
        }

        private async Task<string> SaveTicketImageToBlobStorage(string base64QRImage, TicketBodyQRDto ticketDto, string fileName)
        {
            Bitmap ticketResultImage = await EscribirPlantilla(base64QRImage, ticketDto);
            using MemoryStream stream = new MemoryStream();
            ticketResultImage.Save(stream, ImageFormat.Png);
            byte[] ticketResultBytes = stream.ToArray();

            await _blobFilesService.AddTicketQRBlobStorage(ticketResultBytes, fileName);
            return Convert.ToBase64String(ticketResultBytes);
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

        public async Task<Bitmap> EscribirPlantilla(string base64Image, TicketBodyQRDto ticket)
        {
            string projectRootPath = _hostEnviroment.ContentRootPath;
            string htmlTemplatePath = Path.Combine(projectRootPath, "Template", "IaETicket.html");
            string htmlTemplate = File.ReadAllText(htmlTemplatePath);

            string formatDay = ticket.Evento.Fecha.ToString("dddd", new CultureInfo("es-ES"));
            string anio = ticket.Evento.Fecha.ToString("yyyy", new CultureInfo("es-ES"));
            string formatDate = ticket.Evento.Fecha.ToString("d' de 'MMMM", new CultureInfo("es-ES"));
            string formatHora = ticket.Evento.Fecha.ToString("HH:mm");
            long montoTotalFormat = (long)ticket.MontoTotal;
            string pais = "Chile";
            string comuna = ticket.Sector.Lugar.Comuna.NombreComuna;
          

            string htmlWithImage = htmlTemplate.Replace("{Base64Image}", base64Image)
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
            Bitmap htmlAsBitmap = await _qrGeneratorService.RenderHtmlToBitmapAsync(htmlWithImage);
            return htmlAsBitmap;

        }
    }
}
