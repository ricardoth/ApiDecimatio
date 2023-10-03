namespace Decimatio.Infraestructure.Services
{
    public class LugarService : ILugarService
    {
        private readonly ILugarRepository _lugarRepository;

        public LugarService(ILugarRepository lugarRepository)
        {
            _lugarRepository = lugarRepository;
        }

        public async Task<IEnumerable<Lugar>> GetAllLugares()
        {
            try
            {
                return await _lugarRepository.GetAllLugares();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener la lista de lugares {ex.Message}");
            }
        }
    }
}
