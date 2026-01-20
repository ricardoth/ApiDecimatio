using Decimatio.Common.Interfaces;

namespace Decimatio.Application.Services
{
    internal sealed class MedioPagoService : IMedioPagoService
    {
        private readonly IMedioPagoRepository _medioPagoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;
        private readonly IMapper _mapper;

        public MedioPagoService(IMedioPagoRepository medioPagoRepository,
            IBlobFilesService blobFilesService,
            BlobContainerConfig containerConfig,
            IMapper mapper)
        {
            _medioPagoRepository = medioPagoRepository;
            _blobFilesService = blobFilesService;   
            _containerConfig = containerConfig;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedioPagoDto>> GetMediosPagosAsync()
        {
            var result = await _medioPagoRepository.GetMedioPagos();

            if (result == null)
                throw new NoContentException();

            var tasks = result.Select(async mPago =>
            {
                string imageNamePath = _containerConfig.FolderMedioPago + mPago.UrlImageBlob;
                mPago.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return mPago;
            });

            var mediosPagos = await Task.WhenAll(tasks);
            var medioPagoDtos = _mapper.Map<IEnumerable<MedioPagoDto>>(mediosPagos);
            return medioPagoDtos;
        }

        public async Task<MedioPagoDto> GetMedioPagoAsync(int id)
        {
            var result = await _medioPagoRepository.GetMedioPago(id);
            if (result == null)
                throw new NoContentException("No existe el evento solicitado");

            string imageNamePath = _containerConfig.FolderMedioPago + result.UrlImageBlob;
            result.UrlImageBlob = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);

            var medioPagoDto = _mapper.Map<MedioPagoDto>(result);
            return medioPagoDto;
        }

        public async Task AddMedioPagoAsync(MedioPagoDto medioPagoDto)
        {
            var medioPago = _mapper.Map<MedioPago>(medioPagoDto);

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

        public async Task<bool> UpdateMedioPagoAsync(MedioPagoDto medioPagoDto)
        {
            var medioPago = _mapper.Map<MedioPago>(medioPagoDto);
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
