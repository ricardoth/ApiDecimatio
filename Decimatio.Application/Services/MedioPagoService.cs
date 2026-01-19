using Decimatio.Common.Interfaces;
using Decimatio.Domain.ValueObjects;

namespace Decimatio.Application.Services
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
            var result = await _medioPagoRepository.GetMedioPagos();

            var tasks = result.Select(async mPago =>
            {
                string imageNamePath = _containerConfig.FolderMedioPago + mPago.UrlImageBlob;
                mPago.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return mPago;
            });
            return await Task.WhenAll(tasks);
        }

        public async Task<MedioPago> GetMedioPagoAsync(int id)
        {
            var result = await _medioPagoRepository.GetMedioPago(id);
            if (result == null)
                throw new Exception("Ha ocurrido un error al obtener el evento desde el Repositorio");

            string imageNamePath = _containerConfig.FolderMedioPago + result.UrlImageBlob;
            result.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
            return result;
        }

        public async Task AddMedioPagoAsync(MedioPago medioPago)
        {
            if (medioPago.UrlImageBlob is not null || medioPago.UrlImageBlob != "")
            {
                string imageNamePath = _containerConfig.FolderMedioPago + medioPago.NombreMedioPago;
                var flyerContent = Convert.FromBase64String(medioPago.UrlImageBlob);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }
            medioPago.UrlImageBlob = medioPago.NombreMedioPago;
            await _medioPagoRepository.AddMedioPago(medioPago);
        }

        public async Task<bool> DeleteMedioPagoAsync(int id)
        {
            if (id <= 0)
                throw new NotFoundException("El Medio de Pago debe ser válido");
           
            var result = await _medioPagoRepository.DeleteMedioPago(id);
            return result;
        }

        public async Task<bool> UpdateMedioPagoAsync(MedioPago medioPago)
        {
            if (medioPago.IdMedioPago <= 0)
                throw new NotFoundException("El Medio de Pago debe ser válido");

            if (medioPago.UrlImageBlob is not null || medioPago.UrlImageBlob != "")
            {
                string imageNamePath = _containerConfig.FolderMedioPago + medioPago.NombreMedioPago;
                var flyerContent = Convert.FromBase64String(medioPago.UrlImageBlob);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }
            medioPago.UrlImageBlob = medioPago.NombreMedioPago;
            var result = await _medioPagoRepository.UpdateMedioPago(medioPago);
            return result;
        }
    }
}
