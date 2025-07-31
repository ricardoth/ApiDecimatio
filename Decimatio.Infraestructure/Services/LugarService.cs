namespace Decimatio.Infraestructure.Services
{
    internal sealed class LugarService : ILugarService
    {
        private readonly ILugarRepository _lugarRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;
        private readonly IMapper _mapper;

        public LugarService(ILugarRepository lugarRepository, IBlobFilesService blobFilesService, BlobContainerConfig containerConfig, IMapper mapper)
        {
            _lugarRepository = lugarRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LugarDto>> GetAllLugares()
        {
            var result = await _lugarRepository.GetAllLugares();
            var tasks = result.Select(async lugar =>
            {
                string imageNamePath = _containerConfig.ReferencialMapName + lugar.MapaReferencial;
                lugar.UrlImagenMapaReferencial = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return lugar;
            });

            var lugares = await Task.WhenAll(tasks);
            var mappedLugares = _mapper.Map<IEnumerable<LugarDto>>(lugares);
            return mappedLugares;
        }

        public async Task<LugarDto> GetById(int idLugar)
        {
            var result = await _lugarRepository.GetById(idLugar);
            if (result is null)
                throw new NoContentException("No se encontró el Lugar solicitado");

            string imageNamePath = _containerConfig.ReferencialMapName + result.MapaReferencial;
            result.UrlImagenMapaReferencial = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
            var mappedLugar = _mapper.Map<LugarDto>(result);
            return mappedLugar;
        }

        public async Task AddLugar(Lugar lugar)
        {
            if (!string.IsNullOrEmpty(lugar.Base64ImagenMapaReferencial))
            {
                string imageNamePath = _containerConfig.ReferencialMapName + lugar.NombreMapaReferencial;
                var flyerContent = Convert.FromBase64String(lugar.Base64ImagenMapaReferencial);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }
            await _lugarRepository.AddLugar(lugar);
        }

        public async Task<bool> UpdateLugar(UpdateLugarDto updateLugarDto)
        {
            if (!string.IsNullOrEmpty(updateLugarDto.Base64ImagenMapaReferencial))
            {
                string imageNamePath = _containerConfig.ReferencialMapName + updateLugarDto.NombreMapaReferencial;
                var flyerContent = Convert.FromBase64String(updateLugarDto.Base64ImagenMapaReferencial);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }

            var lugarBd = await _lugarRepository.GetById(updateLugarDto.IdLugar);
            var lugar = _mapper.Map(updateLugarDto, lugarBd);
            
            return await _lugarRepository.UpdateLugar(lugar);
        }

        public async Task<bool> DeleteLugar(int idLugar)
        {
            return await _lugarRepository.DeleteLugar(idLugar);
        }
    }
}
