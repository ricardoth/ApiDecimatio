
using Decimatio.Application.DTOs;

namespace Decimatio.Application.Interfaces.Services
{
    public interface ISectorService 
    {
        Task<IEnumerable<SectorDto>> GetAllSectores();
        Task<IEnumerable<SectorDto>> GetSectoresByEvento(int idEvento);
        Task<SectorDto> GetById(long idSector);
        Task AddSector(CreateSectorDto createSectorDto);
        Task<bool> UpdateSector(UpdateSectorDto updateSectorDto);
        Task<bool> DeleteSector(int idSector);
    }
}
