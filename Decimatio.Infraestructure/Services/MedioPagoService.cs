namespace Decimatio.Infraestructure.Services
{
    public class MedioPagoService : IMedioPagoService
    {
        private readonly IMedioPagoRepository _medioPagoRepository;

        public MedioPagoService(IMedioPagoRepository medioPagoRepository)
        {
            _medioPagoRepository = medioPagoRepository;
        }

        public async Task<List<MedioPago>> GetMediosPagosAsync()
        {
            List<MedioPago> medioPagos = new List<MedioPago>();

            try
            {
                medioPagos = (List<MedioPago>)await _medioPagoRepository.GetMedioPagos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error, mensaje: {ex.Message}", ex);
            }
            return medioPagos;

        }

        public async Task AddMedioPago(MedioPago medioPago)
        {
            try
            {
                await _medioPagoRepository.AddMedioPago(medioPago);
            }
            catch (Exception ex)
            {

                throw new Exception($"Se ha producido un error, mensaje: {ex.Message}", ex);
            }
        }

        public async Task<MedioPago> GetMedioPagoAsync(int id)
        {
            try
            {
                var result = await _medioPagoRepository.GetMedioPago(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error, mensaje: {ex.Message}", ex);
            }
        }

        public async Task AddMedioPagoAsync(MedioPago medioPago)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteMedioPagoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMedioPagoAsync(int id, MedioPago medioPago)
        {
            throw new NotImplementedException();
        }
    }
}
