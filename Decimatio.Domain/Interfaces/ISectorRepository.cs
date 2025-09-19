namespace Decimatio.Domain.Interfaces
{
    public interface ISectorRepository
    {
        Task<IEnumerable<Sector>> GetAllSectores();
        Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento);
        Task<Sector> GetById(long idSector);
        Task<int> AddSector(Sector sector);
        Task<bool> UpdateSector(Sector sector);
        Task<bool> DeleteSector(long idSector);
    }
}
