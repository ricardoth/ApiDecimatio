using Decimatio.Common.DTOs;
using Decimatio.Common.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Decimatio.Application.Services
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
        private readonly IPreferenceRepository _preferenceRepository;

        public TicketService(ITicketRepository ticketRepository,
            IQRGeneratorService qRGeneratorService,
            IPDFGeneratorService pdfGeneratorService,
            IBlobFilesService blobFilesService,
            IMapper mapper,
            IEmailSenderService emailSenderService,
            IOptions<PaginationOptions> paginationOptions,
            BlobContainerConfig containerConfig,
            IPreferenceRepository preferenceRepository)
        {
            _ticketRepository = ticketRepository;
            _qrGeneratorService = qRGeneratorService;
            _pdfGeneratorService = pdfGeneratorService;
            _mapper = mapper;
            _blobFilesService = blobFilesService;
            _emailSenderService = emailSenderService;
            _paginationOptions = paginationOptions.Value;
            _containerConfig = containerConfig;
            _preferenceRepository = preferenceRepository;
        }

        #region Get All Tickets y QR

        public async Task<(IEnumerable<TicketDto>, MetaData)> GetAllTickets(TicketQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            var tickets = await _ticketRepository.GetAllTicket(filtros);
            var counters = await _ticketRepository.GetCounterTicket();
            var pagedTickets = PagedList<Ticket>.CreatePaginationFromDb(tickets, counters, filtros.PageNumber, filtros.PageSize);

            var metaData = new MetaData
            {
                TotalCount = pagedTickets.TotalCount,
                PageSize = pagedTickets.PageSize,
                CurrentPage = pagedTickets.CurrentPage,
                TotalPages = pagedTickets.TotalPages,
                HasNextPage = pagedTickets.HasNextPage,
                HasPreviousPage = pagedTickets.HasPreviousPage,
                NextPageUrl = "",// _uriService.GetMenuPaginationUri(filtros, Url.RouteUrl(nameof(Get))).ToString(),
                PreviousPageUrl = "",//_uriService.GetMenuPaginationUri(filtros, Url.RouteUrl(nameof(Get))).ToString()
            };
            var ticketsDtos = _mapper.Map<IEnumerable<TicketDto>>(pagedTickets);
            return (ticketsDtos, metaData);
        }

        public async Task<TicketQRDto> GetTicketQR(int idTicket)
        {
            var ticket = await _ticketRepository.GetTicketQR(idTicket);
            if (ticket is null)
                throw new NoContentException("No se encontró el ticket solicitado");

            var ticketQRDto = _mapper.Map<TicketQRDto>(ticket);
            return ticketQRDto; 
        }

        public async Task<TicketQRDto> GetTicketVoucherPDF(int idTicket)
        {
            var ticket = await _ticketRepository.GetInfoTicket(idTicket);
            var ticketDto = _mapper.Map<TicketBodyQRDto>(ticket);

            string fileName = GetTicketFileName(ticketDto);
            var ticketQR = await GetTicketQR(idTicket);

            var comprobante = await _blobFilesService.GetImageFromBlobStorage(fileName);
            ticketQR.NombreTicketComprobante = comprobante;

            var ticketQRDto = _mapper.Map<TicketQRDto>(ticketQR);

            if (ticketQRDto is null)
                throw new NoContentException("No se encontró el ticket solicitado");

            return ticketQRDto;
        }
        #endregion


        #region Agregar Ticket
        public async Task<string> AddTicket(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            var result = await _ticketRepository.AddTicket(ticket);
            if (result == 0)
                throw new Exception($"Ha ocurrido un error al guardar el ticket");

            Ticket ticketWithInfo = await _ticketRepository.GetInfoTicket(result);
            var ticketBodyQRDto = _mapper.Map<TicketBodyQRDto>(ticketWithInfo);

            var json = new TicketInfoDto()
            {
                IdTicket = result,
                FechaTicket = ticketBodyQRDto.FechaTicket,
                MontoTotal = ticketBodyQRDto.MontoTotal,
                RutUsuario = $"{ticketBodyQRDto?.Usuario?.Rut}-{ticketBodyQRDto?.Usuario?.DV}",
                Nombres = ticketBodyQRDto?.Usuario?.Nombres,
                ApellidoP = ticketBodyQRDto?.Usuario?.ApellidoP,
                ApellidoM = ticketBodyQRDto.Usuario.ApellidoM,
                Correo = ticketBodyQRDto.Usuario.Correo,
                IdEvento = ticketBodyQRDto.Evento?.IdEvento,
                IdSector = ticketBodyQRDto.Sector?.IdSector
            };

            string base64QRImage = GeneratoQRCodeBase64(json);

            TicketQR ticketQR = new TicketQR()
            {
                IdTicket = result,
                Contenido = base64QRImage,
                Ticket = ticket,
            };

            string fileName = GetTicketFileName(ticketBodyQRDto);

            ticketQR.NombreTicketComprobante = fileName;
            await AddTicketQR(ticketQR);
            string base64HtmlTicket = await SaveTicketImageToBlobStorage(base64QRImage, ticketBodyQRDto, fileName);
            string emailToName =$"{ticketBodyQRDto.Evento.NombreEvento} - {ticketBodyQRDto.IdTicket}";

            var emailDto = new RequestEmailTicketDto()
            {
                To = json.Correo,
                Subject = emailToName,
                Base64 = base64HtmlTicket,
                IdTicket = ticketBodyQRDto.IdTicket,
                Nombres = ticketBodyQRDto.Usuario.Nombres,
                ApellidoPaterno = ticketBodyQRDto.Usuario.ApellidoP,
                ApellidoMaterno = ticketBodyQRDto.Usuario.ApellidoM,
                NombreEvento = ticketBodyQRDto.Evento.NombreEvento,
                NombreLugar = ticketBodyQRDto.Evento.Lugar.NombreLugar,
                NombreSector = ticketBodyQRDto.Sector.NombreSector,
                MontoTotal = (long)ticketBodyQRDto.MontoTotal
            };
             
            var resultEmail = await _emailSenderService.SendEmailTicket(emailDto);
            if (resultEmail != "")
            {
                //Traza de que no se envío el correo
            }

            return base64HtmlTicket;
        }

        public async Task<TicketQR> AddTicketQR(TicketQR ticketQR)
        {
            if (ticketQR == null)
                throw new ArgumentNullException(nameof(ticketQR));

            await _ticketRepository.AddTicketQR(ticketQR);
            return ticketQR;
        }

        public async Task<bool> DeleteDownTicket(long idTicket, bool activo)
        {
            var result = await _ticketRepository.DeleteDownTicket(idTicket, activo);
            return result;
        }
        #endregion

        #region Agregar Más de un Ticket
        public async Task<string> AddTickets(IEnumerable<TicketDto> ticketsDto)
        {
            var tickets = _mapper.Map<IEnumerable<Ticket>>(ticketsDto);
            List<string> strList = new List<string>();
          
            if (!tickets.Any())
                throw new Exception("No se encontraron tickets para generar");

            foreach (var ticket in tickets)
            {
                var mappedTicket = _mapper.Map<TicketDto>(ticket);  
                var objTicket = await AddTicket(mappedTicket);
                strList.Add(objTicket.ToString());
            }

            string ticketsPdf64 = _pdfGeneratorService.CombinePdfFiles(strList);
            if (ticketsPdf64 is null)
                throw new BadRequestException("No se pudieron generar los tickets");

            return ticketsPdf64;
        }
        #endregion

        #region Creación QR y Generación del PDF
        public async Task<byte[]> EscribirPlantilla(string base64Image, TicketBodyQRDto ticket)
        {
            var requestTicketBody = new RequestTicketBodyQRDto()
            {
                IdTicket = ticket.IdTicket,
                FechaEvento = ticket.Evento.Fecha,
                MontoPago = ticket.MontoPago,
                MontoTotal = ticket.MontoTotal,
                FechaTicket = ticket.FechaTicket,
                NombreComuna = ticket.Evento.Lugar.Comuna.NombreComuna,
                NombreEvento = ticket.Evento.NombreEvento,
                NombreLugar = ticket.Evento.Lugar.NombreLugar,
                NombreSector = ticket.Sector.NombreSector,
                Numeracion = ticket.Evento.Lugar.Numeracion,
                ProductoraResponsable = ticket.Evento.ProductoraResponsable
            };
            var htmlPdfQuest = _pdfGeneratorService.GeneratePDFVoucher(base64Image, requestTicketBody);
            return htmlPdfQuest;
        }

        private string GeneratoQRCodeBase64(TicketInfoDto ticket)
        {
            using MemoryStream memoryStream = new();
            Bitmap qrCodeImage = _qrGeneratorService.GenerateQRCodeTicket(ticket);
            qrCodeImage.Save(memoryStream, ImageFormat.Png);
            byte[] imageBytes = memoryStream.ToArray();
            memoryStream.Close();
            return Convert.ToBase64String(imageBytes);
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

        public async Task<IEnumerable<PreferenceTicketDto>> GetPreferenceTicketsByTransaction(string transactionId)
        {
            if (transactionId is null || transactionId == "")
                throw new NotFoundException("El transactionId no es válido");

            var result = await _ticketRepository.GetPreferenceTicketsByTransaction(transactionId);
            if (!result.Any())
                throw new BadRequestException("No existen tickets asociados a la transacción");

            var preferenceTicketsDto = _mapper.Map<IEnumerable<PreferenceTicketDto>>(result);

            return preferenceTicketsDto;
        }

        public async Task<IEnumerable<PreferenceTicketDto>> GetAllPreferenceTickets()
        {
            var results = await _preferenceRepository.GetAll();
            if (!results.Any())
                throw new NotFoundException("No se encontraron registros");

            var preferenceTicketsDtos = _mapper.Map<IEnumerable<PreferenceTicketDto>>(results);
            return preferenceTicketsDtos;
        }
        #endregion

        #region Exportar Excel Historial Tickets
        public async Task<IEnumerable<TicketDto>> GetAllTicketsExcel(TicketQueryFilter filtros)
        {
            var tickets = await _ticketRepository.GetAllTicketReport();

            if (filtros.IdEvento > 0)
                tickets = tickets.Where(x => x.IdEvento == filtros.IdEvento);

            if (filtros.IdSector > 0)
                tickets = tickets.Where(x => x.IdSector == filtros.IdSector);

            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);
            var result = ticketsDto.Where(x => x.Activo == true);
            if (!result.Any())
                throw new NoContentException("No se encuentran registros para exportar");

            return result;
        }
        #endregion

        public async Task<string> AddQueueTicket(string preferenceCode)
        {
            var newTickets = new List<Ticket>();
            var tickets = await _preferenceRepository.GetByPreferenceCode(preferenceCode);
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

            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(newTickets);
            var result = await AddTickets(ticketsDto);
            await _ticketRepository.UpdateTicketsDownload(tickets.FirstOrDefault().TransactionId);
            return result;
        }
    }
}
