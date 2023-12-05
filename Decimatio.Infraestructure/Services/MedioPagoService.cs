namespace Decimatio.Infraestructure.Services
{
    internal sealed class MedioPagoService : IMedioPagoService
    {
        private readonly IMedioPagoRepository _medioPagoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;

        public MedioPagoService(IMedioPagoRepository medioPagoRepository,
            IBlobFilesService blobFilesService,
            BlobContainerConfig containerConfig)
        {
            _medioPagoRepository = medioPagoRepository;
            _blobFilesService = blobFilesService;   
            _containerConfig = containerConfig;
        }

        public async Task<IEnumerable<MedioPago>> GetMediosPagosAsync()
        {

            try
            {
                var result = await _medioPagoRepository.GetMedioPagos();

                var tasks = result.Select(async mPago =>
                {
                    string imageNamePath = _containerConfig.FolderMedioPago + mPago.UrlImageBlob;
                    mPago.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                    return mPago;
                });
                return await Task.WhenAll(tasks);
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
                if (result == null)
                    throw new Exception("Ha ocurrido un error al obtener el evento desde el Repositorio");

                string imageNamePath = _containerConfig.FolderMedioPago + result.UrlImageBlob;
                result.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error, mensaje: {ex.Message}", ex);
            }
        }

        public async Task AddMedioPagoAsync(MedioPago medioPago)
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
