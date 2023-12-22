namespace Decimatio.Infraestructure.Services
{
    public sealed class MercadoPagoService : IMercadoPagoService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly MercadoPagoOptions _mercadoPagoOptions;

        public MercadoPagoService(ITicketRepository ticketRepository, IOptions<MercadoPagoOptions> options)
        {
            _ticketRepository = ticketRepository;
            _mercadoPagoOptions = options.Value;
            MercadoPagoConfig.AccessToken = _mercadoPagoOptions.AccessToken;
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
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = $"{_mercadoPagoOptions.BackUrl}/successShop?transactionId={transactionId}",
                        Failure = $"{_mercadoPagoOptions.BackUrl}/failureShop?transactionId={transactionId}",
                        Pending = $"{_mercadoPagoOptions.BackUrl}/pendingShop?transactionId={transactionId}"
                    },
                    AutoReturn = "approved",
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