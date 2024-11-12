namespace Decimatio.Infraestructure.Services
{
    internal sealed class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IPDFGeneratorService _pdfGeneratorService;
        private readonly IBlobFilesService _blobFilesService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly BlobContainerConfig _containerConfig;
        private readonly IMercadoPagoRepository _mercadoPagoRepository;

        public TicketService(ITicketRepository ticketRepository, 
            IQRGeneratorService qRGeneratorService, 
            IPDFGeneratorService pdfGeneratorService,
            IBlobFilesService blobFilesService, 
            IMapper mapper, 
            IEmailSenderService emailSenderService,
            IOptions<PaginationOptions> paginationOptions, 
            BlobContainerConfig containerConfig
            , IMercadoPagoRepository mercadoPagoRepository)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _pdfGeneratorService = pdfGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
            _emailSenderService = emailSenderService;
            _paginationOptions = paginationOptions.Value;
            _containerConfig = containerConfig;
            _mercadoPagoRepository = mercadoPagoRepository;
        }

        #region Get All Tickets y QR

        public async Task<PagedList<Ticket>> GetAllTickets(TicketQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            try
            {
                var tickets = await _ticketRepository.GetAllTicket(filtros);
                var counters = await _ticketRepository.GetCounterTicket();
                var pagedTickets = PagedList<Ticket>.CreatePaginationFromDb(tickets, counters, filtros.PageNumber, filtros.PageSize);
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

        public async Task<TicketQR> GetTicketVoucherPDF(int idTicket)
        {
            try
            {
                var ticket = await _ticketRepository.GetInfoTicket(idTicket);
                var ticketDto = _mapper.Map<TicketBodyQRDto>(ticket);

                string fileName = GetTicketFileName(ticketDto);
                var ticketQR = await GetTicketQR(idTicket);

                var comprobante = await _blobFilesService.GetImageFromBlobStorage(fileName);
                ticketQR.NombreTicketComprobante = comprobante;

                return ticketQR;
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
                    throw new Exception($"Ha ocurrido un error al guardar el ticket");

                Ticket ticketWithInfo = await _ticketRepository.GetInfoTicket(result);
                var ticketDto = _mapper.Map<TicketBodyQRDto>(ticketWithInfo);

                var json = new TicketInfoDto()
                {
                    IdTicket = result,
                    FechaTicket = ticketDto.FechaTicket,
                    MontoTotal = ticketDto.MontoTotal,
                    RutUsuario = $"{ticketDto?.Usuario?.Rut}-{ticketDto?.Usuario?.DV}",
                    Nombres = ticketDto?.Usuario?.Nombres,
                    ApellidoP = ticketDto?.Usuario?.ApellidoP,
                    ApellidoM = ticketDto.Usuario.ApellidoM,
                    Correo = ticketDto.Usuario.Correo,
                    IdEvento = ticketDto.Evento?.IdEvento,
                    IdSector = ticketDto.Sector?.IdSector
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
                string emailToName =$"{ticketDto.Evento.NombreEvento} - {ticketDto.IdTicket}";

                var emailDto = new EmailTicketDto()
                {
                    To = json.Correo,
                    Subject = emailToName,
                    Base64 = base64HtmlTicket,
                    IdTicket = ticketDto.IdTicket,
                    Nombres = ticketDto.Usuario.Nombres,
                    ApellidoPaterno = ticketDto.Usuario.ApellidoP,
                    ApellidoMaterno = ticketDto.Usuario.ApellidoM,
                    NombreEvento = ticketDto.Evento.NombreEvento,
                    NombreLugar = ticketDto.Evento.Lugar.NombreLugar,
                    NombreSector = ticketDto.Sector.NombreSector,
                    MontoTotal = (long)ticketDto.MontoTotal
                };
             
                var resultEmail = await _emailSenderService.SendEmailTicket(emailDto);
                if (resultEmail != "")
                {
                    //Traza de que no se envío el correo
                }

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

        public async Task<bool> DeleteDownTicket(long idTicket, bool activo)
        {
            try
            {
                var result = await _ticketRepository.DeleteDownTicket(idTicket, activo);
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
                    throw new Exception("No se encontraron tickets para generar");


                foreach (var ticket in tickets)
                {
                    var objTicket = await AddTicket(ticket);
                    strList.Add(objTicket.ToString());
                }

                string ticketsPdf64 = _pdfGeneratorService.CombinePdfFiles(strList);
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
            var htmlPdfQuest = _pdfGeneratorService.GeneratePDFVoucher(base64Image, ticket);
            return htmlPdfQuest;
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
            string fileName;
            if (ticketDto?.Usuario?.EsExtranjero == false)
                fileName = $"Ticket N° {ticketDto.IdTicket} | Rut: {ticketDto.Usuario.Rut}-{ticketDto.Usuario.DV}.pdf";
            else 
                fileName = $"Ticket N° {ticketDto.IdTicket} | Correo: {ticketDto.Usuario.Correo}.pdf";

            string filePathName = $"{_containerConfig.FolderName}{fileName}";
            return filePathName;
        }

        private async Task<string> SaveTicketImageToBlobStorage(string base64QRImage, TicketBodyQRDto ticketDto, string fileName)
        {
            var ticketResultImage = await EscribirPlantilla(base64QRImage, ticketDto);
            await _blobFilesService.AddTicketQRBlobStorage(ticketResultImage, fileName);
            return Convert.ToBase64String(ticketResultImage);
        }
        #endregion


        #region Preference Tickets 

        public async Task<IEnumerable<PreferenceTicket>> GetPreferenceTicketsByTransaction(string transactionId)
        {
            if (transactionId is null || transactionId == "")
                throw new NotFoundException("El transactionId no es válido");

            var result = await _ticketRepository.GetPreferenceTicketsByTransaction(transactionId);
            if (!result.Any())
                throw new BadRequestException("No existen tickets asociados a la transacción");

            //marcar los descargados para no generar nuevamente los tickets
            //var downloaded = await _ticketRepository.UpdateTicketsDownload(transactionId);
            //if (!downloaded)
            //    throw new BadRequestException("Ya se generaron los tickets asociados a la transacción, por favor verifique su sección Mis Tickets");

            return result;
        }
        #endregion

        #region Exportar Excel Historial Tickets
        public async Task<IEnumerable<Ticket>> GetAllTicketsExcel(TicketQueryFilter filtros)
        {
            try
            {
                var tickets = await _ticketRepository.GetAllTicketReport();

                if (filtros.IdEvento > 0)
                    tickets = tickets.Where(x => x.IdEvento == filtros.IdEvento);

                if (filtros.IdSector > 0)
                    tickets = tickets.Where(x => x.IdSector == filtros.IdSector);


                return tickets.Where(x => x.Activo == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error en TicketService {ex.Message}", ex);
            }
        }

        #endregion

        public async Task<string> AddQueueTicket(string preferenceCode)
        {
            try
            {
                var newTickets = new List<Ticket>();
                var tickets = await _mercadoPagoRepository.GetByPreferenceCode(preferenceCode);
                if (!tickets.Any())
                    throw new NotFoundException("No se encontraron tickets asociados al pago");

                foreach (var item in tickets)
                {
                    var tkt = new Ticket()
                    {
                        IdUsuario = item.IdUsuario,
                        IdEvento = item.IdEvento,
                        IdSector = item.IdSector,
                        IdMedioPago = item.IdMedioPago,
                        MontoPago = item.MontoPago,
                        MontoTotal =  item.MontoTotal,
                        FechaTicket = item.FechaTicket,
                        Activo = true
                    };
                    newTickets.Add(tkt);
                }

                var result = await AddTickets(newTickets);
                await _ticketRepository.UpdateTicketsDownload(tickets.FirstOrDefault().TransactionId);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al Generar los tickets asociados al preferenceId: {preferenceCode}", ex);
            }
        }
    }
}
