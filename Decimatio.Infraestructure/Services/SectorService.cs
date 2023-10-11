namespace Decimatio.Infraestructure.Services
{
    public class SectorService : ISectorService
    {
		private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<Sector>> GetAllSectores()
        {
			try
			{
                var result = await _sectorRepository.GetAllSectores();
                return result;
			}
			catch (Exception ex)
			{

				throw;
			}
        }

        public async Task<Sector> GetById(int idSector)
        {
            try
            {
                var result = await _sectorRepository.GetById(idSector);
                return result;  
            }
            catch (Exception ex)
            {

                throw new Exception($"Ha ocurrido un error en capa Services, error: {ex.Message}" ,ex);
            }
        }

        public async Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento)
        {
            try
            {
                var result = await _sectorRepository.GetSectoresByEvento(idEvento);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener los sectores: {ex.Message}");
            }
        }

        public async Task AddSector(Sector sector)
        {
            try
            {
                await _sectorRepository.AddSector(sector);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al agregar el sector: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateSector(Sector sector)
        {
            try
            {
                var result = await _sectorRepository.UpdateSector(sector);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al actualizar el sector: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteSector(int idSector)
        {
            try
            {
                return await _sectorRepository.DeleteSector(idSector);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al eliminar el sector: {ex.Message}", ex);
            }
        }
    }
}
