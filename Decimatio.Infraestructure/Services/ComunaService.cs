namespace Decimatio.Infraestructure.Services
{
    internal sealed class ComunaService : IComunaService     
    {
        private readonly IComunaRepository _comunaRepository;

        public ComunaService(IComunaRepository comunaRepository)
        {
            _comunaRepository = comunaRepository;        
        }

        public async Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion)
        {
            return await _comunaRepository.GetComunasByRegion(idRegion);
        }
    }
}
