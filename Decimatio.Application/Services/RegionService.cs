namespace Decimatio.Application.Services
{
    internal sealed class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionService(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;        
            _mapper = mapper;
        }

        public async Task<IEnumerable<RegionDto>> GetAllRegions()
        {
            var result = await _regionRepository.GetAllRegions();
            if (!result.Any())
                throw new NoContentException("No se encontraron regiones");

            var regionesDtos = _mapper.Map<IEnumerable<RegionDto>>(result);
            return regionesDtos;
        }
    }
}
