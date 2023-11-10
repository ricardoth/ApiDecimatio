namespace Decimatio.Infraestructure.Services
{
    public class ComunaService : IComunaService     
    {
        private readonly IComunaRepository _comunaRepository;

        public ComunaService(IComunaRepository comunaRepository)
        {
            _comunaRepository = comunaRepository;        
        }

        public async Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion)
        {
            try
            {
                return await _comunaRepository.GetComunasByRegion(idRegion);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se ha podido cargar las comunas, {ex.Message}");
            }
        }
    }
}
