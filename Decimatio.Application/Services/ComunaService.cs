namespace Decimatio.Application.Services
{
    internal sealed class ComunaService : IComunaService     
    {
        private readonly IComunaRepository _comunaRepository;
        private readonly IMapper _mapper;

        public ComunaService(IComunaRepository comunaRepository, IMapper mapper)
        {
            _comunaRepository = comunaRepository;        
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComunaDto>> GetComunasByRegion(int idRegion)
        {
            var result = await _comunaRepository.GetComunasByRegion(idRegion);
            if (!result.Any())
                throw new NotFoundException("No se encontraron comunas para la región solicitada");

            var comunasDtos = _mapper.Map<IEnumerable<ComunaDto>>(result);
            return comunasDtos;
        }
    }
}
