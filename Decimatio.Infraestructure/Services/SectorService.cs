namespace Decimatio.Infraestructure.Services
{
    internal sealed class SectorService : ISectorService
    {
		private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<Sector>> GetAllSectores()
        {
            var result = await _sectorRepository.GetAllSectores();
            return result;
        }

        public async Task<Sector> GetById(int idSector)
        {
            var result = await _sectorRepository.GetById(idSector);
            return result;  
        }

        public async Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento)
        {
            var result = await _sectorRepository.GetSectoresByEvento(idEvento);
            return result;
        }

        public async Task AddSector(Sector sector)
        {
            await _sectorRepository.AddSector(sector);
        }

        public async Task<bool> UpdateSector(Sector sector)
        {
            var result = await _sectorRepository.UpdateSector(sector);
            return result;
        }

        public async Task<bool> DeleteSector(int idSector)
        {
            return await _sectorRepository.DeleteSector(idSector);
        }
    }
}
