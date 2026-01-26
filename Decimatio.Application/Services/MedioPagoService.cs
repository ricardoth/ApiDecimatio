using Decimatio.Common.Interfaces;

namespace Decimatio.Application.Services
{
    internal sealed class MedioPagoService : IMedioPagoService
    {
        private readonly IMedioPagoRepository _medioPagoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateMedioPagoDto> _updateMedioPagoDtoValidator;

        public MedioPagoService(IMedioPagoRepository medioPagoRepository,
            IBlobFilesService blobFilesService,
            BlobContainerConfig containerConfig,
            IMapper mapper,
            IValidator<UpdateMedioPagoDto> updateMedioPagoDtoValidator)
        {
            _medioPagoRepository = medioPagoRepository;
            _blobFilesService = blobFilesService;   
            _containerConfig = containerConfig;
            _mapper = mapper;
            _updateMedioPagoDtoValidator = updateMedioPagoDtoValidator;
        }

        public async Task<IEnumerable<MedioPagoDto>> GetMediosPagosAsync()
        {
            var result = await _medioPagoRepository.GetMedioPagos();

            if (result == null)
                throw new NotFoundException("No se encuentran registros de medios de pago");

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
                throw new NotFoundException("No existe el evento solicitado");

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

        public async Task<bool> UpdateMedioPagoAsync(UpdateMedioPagoDto updateMedioPagoDto)
        {
            var validations = _updateMedioPagoDtoValidator.Validate(updateMedioPagoDto);
            if (!validations.IsValid)
            {
                var errores = validations.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            if (updateMedioPagoDto.IdMedioPago <= 0)
                throw new NotFoundException("El Medio de Pago debe ser válido");

            if (updateMedioPagoDto.UrlImageBlob is not null)
            {
                string imageNamePath = _containerConfig.FolderMedioPago + updateMedioPagoDto.NombreMedioPago;
                var flyerContent = Convert.FromBase64String(updateMedioPagoDto.UrlImageBlob);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
                updateMedioPagoDto.UrlImageBlob = updateMedioPagoDto.NombreMedioPago;
            }

            var medioPagoBd = await _medioPagoRepository.GetMedioPago((int)updateMedioPagoDto.IdMedioPago);
            var medioPago = _mapper.Map(updateMedioPagoDto, medioPagoBd);

            return await _medioPagoRepository.UpdateMedioPago(medioPago);
        }
    }
}
