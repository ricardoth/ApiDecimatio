namespace Decimatio.Infraestructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IBlobFilesService _blobFilesService;
        private readonly IEmailService _emailService;   
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public TicketService(ITicketRepository ticketRepository, IQRGeneratorService qRGeneratorService, 
            IBlobFilesService blobFilesService, IMapper mapper, IEmailService emailService,
            IOptions<PaginationOptions> paginationOptions)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
            _emailService = emailService;
            _paginationOptions = paginationOptions.Value;
        }

        #region Get All Tickets y QR

        public async Task<PagedList<Ticket>> GetAllTickets(TicketQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;
            try
            {

                var tickets = await _ticketRepository.GetAllTicket();

                if (filtros.IdTicket > 0)
                    tickets = tickets.Where(x => x.IdTicket == filtros.IdTicket);

                if (filtros.IdUsuario > 0)
                    tickets = tickets.Where(x => x.IdUsuario == filtros.IdUsuario);

                if (filtros.IdEvento > 0)
                    tickets = tickets.Where(x => x.IdEvento == filtros.IdEvento);

                if (filtros.IdSector > 0)
                    tickets = tickets.Where(x => x.IdSector == filtros.IdSector);

                var pagedTickets = PagedList<Ticket>.Create(tickets, filtros.PageNumber, filtros.PageSize);
                return pagedTickets;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error en TicketService {ex.Message}", ex);
            }        
        }

        public async Task<TicketQR> GetTicketQR(int idTicket)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketQR(idTicket);
                return ticket; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error en TicketService {ex.Message}", ex);
            }
        }
        #endregion


        #region Agregar Ticket
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

                string fileName = GetTicketFileName(ticketDto);

                ticketQR.NombreTicketComprobante = fileName;
                await AddTicketQR(ticketQR);
                string base64HtmlTicket = await SaveTicketImageToBlobStorage(base64QRImage, ticketDto, fileName);

                return base64HtmlTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al Crear el Ticket", ex);
            }
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

        public async Task<bool> DeleteDownTicket(long idTicket)
        {
            try
            {
                var result = await _ticketRepository.DeleteDownTicket(idTicket);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar el Ticket, Ha ocurrido un error en Services", ex);
            }
        }
        #endregion

        #region Agregar Más de un Ticket
        public async Task<string> AddTickets(IEnumerable<Ticket> tickets)
        {
            List<string> strList = new List<string>();
            try
            {
                if (!tickets.Any())
                    return null;


                foreach (var ticket in tickets)
                {
                    var objTicket = await AddTicket(ticket);
                    strList.Add(objTicket.ToString());
                }

                string ticketsPdf64 = await _qrGeneratorService.MergePdfFiles(strList);
                return ticketsPdf64;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Error al Crear el Ticket", ex);
            }
        }
        #endregion


        #region Creación QR y Generación del PDF
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
            string comuna = ticket.Evento.Lugar.Comuna.NombreComuna;


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
                                    .Replace("{Direccion}", ticket.Evento.Lugar.Ubicacion)
                                    .Replace("{Numeracion}", ticket.Evento.Lugar.Numeracion)
                                    .Replace("{HoraEvento}", formatHora)
                                    .Replace("{NombreLugar}", ticket.Evento.Lugar.NombreLugar)
                                    .Replace("{NombreEventoUser}", ticket.Evento.NombreEvento)
                                    .Replace("{Titulo}", ticket.Evento.NombreEvento)
                                    .Replace("{NombreSectorUser}", ticket.Sector.NombreSector)
                                    .Replace("{HoraEventoUser}", formatHora)
                                    .Replace("{IdTicket}", ticket.IdTicket.ToString())
                                    .Replace("{IdTicketUser}", ticket.IdTicket.ToString());
            var htmlPdf = await _qrGeneratorService.RenderHtmlToPdfAsync(htmlWithImage);

            return htmlPdf;

        }

        private string GeneratoQRCodeBase64(TicketInfoDto ticket)
        {
            using MemoryStream memoryStream = new();
            try
            {
                Bitmap qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticket);
                qrCodeImage.Save(memoryStream, ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al generar el TicketQR");
            }
            finally
            {
                memoryStream.Close();
            }
           
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

        
        #endregion
    }
}
