using Decimatio.Domain.MercadoPagoEntitites;

namespace Decimatio.Infraestructure.Services
{
    public sealed class MercadoPagoService : IMercadoPagoService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMercadoPagoRepository _mercadoPagoRepository;
        private readonly MercadoPagoOptions _mercadoPagoOptions;

        public MercadoPagoService(ITicketRepository ticketRepository,
            IMercadoPagoRepository mercadoPagoRepository,   
            IOptions<MercadoPagoOptions> options)
        {
            _ticketRepository = ticketRepository;
            _mercadoPagoRepository = mercadoPagoRepository;
            _mercadoPagoOptions = options.Value;
            MercadoPagoConfig.AccessToken = _mercadoPagoOptions.AccessToken;
        }

        public async Task<int> CrearNotificacionPago(MercadoPagoNotification notification)
        {
            try
            {
                var result = await _mercadoPagoRepository.AddNotificationPayment(notification);
                return result;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"No se pudo insertar la notificación");
            }
        }

        public async Task<Preference> CrearSolicitudPago(PreferenceData data)
        {
            try
            {
                string transactionId = Guid.NewGuid().ToString();
                var preferenceRequest = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
                    {
                        new PreferenceItemRequest
                        {
                            Title = data.Description,
                            UnitPrice = data.Price,
                            Quantity = data.Quantity
                        }
                    },
                    //Payer = new PreferencePayerRequest
                    //{ 
                    //    Name = $"{data.Tickets.FirstOrDefault().Usuario.Nombres} {data.Tickets.FirstOrDefault().Usuario.ApellidoP}",
                    //    Email = data.Tickets.FirstOrDefault().Usuario.Correo,
                    //    Identification = new MercadoPago.Client.Common.IdentificationRequest
                    //    {
                    //        Type = "CI",
                    //        Number = $"{data.Tickets.FirstOrDefault().Usuario.Rut}-{data.Tickets.FirstOrDefault().Usuario.DV}"
                    //    }
                    //},
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = $"{_mercadoPagoOptions.BackUrl}/successShop?transactionId={transactionId}",
                        Failure = $"{_mercadoPagoOptions.BackUrl}/failureShop?transactionId={transactionId}",
                        Pending = $"{_mercadoPagoOptions.BackUrl}/pendingShop?transactionId={transactionId}"
                    },
                    AutoReturn = "approved",
                    NotificationUrl = $"https://api-pagos-dev.azurewebsites.net/api/Notification"
                };

                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(preferenceRequest);

                var result = await AddPreferenceTicket(data.Tickets, transactionId, preference.Id);
                return preference;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error al crear el pago: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PreferenceTicket>> GetAllPreferenceTickets()
        {
            try
            {
                var results = await _mercadoPagoRepository.GetAll();
                if (!results.Any())
                    throw new NotFoundException("No se encontraron registros");

                return results;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error al obtener los datos de mercado pago: {ex.Message}");
            }
        }

        private async Task<bool> AddPreferenceTicket(List<TicketDto> tickets, string transactionId, string preferenceId)
        {
            foreach (var item in tickets)
            {
                var preferenceTicket = new PreferenceTicket()
                {
                    PreferenceCode = preferenceId,
                    TransactionId = transactionId,
                    IdUsuario = (int)item.IdUsuario,
                    IdEvento = (int)item.IdEvento,
                    IdSector = (int)item.IdSector,
                    IdMedioPago = (int)item.IdMedioPago,
                    MontoPago = item.MontoPago,
                    MontoTotal = item.MontoTotal,
                    FechaTicket = item.FechaTicket,
                    Descargados = false,
                    Activo = item.Activo
                };

                var result = await _ticketRepository.AddPreferenceTicket(preferenceTicket);
                if (!result)
                {
                    // registrar en log (insights) los posibles ticket con errores/no insertados
                    throw new BadRequestException($"Error al registrar el pago {transactionId}");
                } 
            }

            return true;
        }
    }
}