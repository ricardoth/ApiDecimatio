namespace Decimatio.Infraestructure.Services
{
    public sealed class MercadoPagoService : IMercadoPagoService
    {
        private readonly IMercadoPagoRepository _mercadoPagoRepository;
        private readonly MercadoPagoOptions _mercadoPagoOptions;

        public MercadoPagoService(IMercadoPagoRepository mercadoPagoRepository,   
            IOptions<MercadoPagoOptions> options)
        {
            _mercadoPagoRepository = mercadoPagoRepository;
            _mercadoPagoOptions = options.Value;
            MercadoPagoConfig.AccessToken = _mercadoPagoOptions.AccessToken;
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
    }
}