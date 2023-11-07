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

        public async Task<Lugar> GetById(int idLugar)
        {
            try
            {
                return await _lugarRepository.GetById(idLugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener el lugar {ex.Message}");
            }
        }

        public async Task AddLugar(Lugar lugar)
        {
            try
            {
                await _lugarRepository.AddLugar(lugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo agregar el lugar {ex.Message}"); 
            }
        }

        public async Task<bool> UpdateLugar(Lugar lugar)
        {
            try
            {
                return await _lugarRepository.UpdateLugar(lugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo editar el lugar {ex.Message}");
            }
        }

        public async Task<bool> DeleteLugar(int idLugar)
        {
            try
            {
                return await _lugarRepository.DeleteLugar(idLugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo eliminar el lugar {ex.Message}");
            }
        }
    }
}
