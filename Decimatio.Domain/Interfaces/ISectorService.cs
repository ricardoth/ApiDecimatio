namespace Decimatio.Domain.Interfaces
{
    public interface ISectorService 
    {
        Task<IEnumerable<Sector>> GetAllSectores();
        Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento);
        Task<Sector> GetById(int idSector);
        Task AddSector(Sector sector);
        Task<bool> UpdateSector(Sector sector);
        Task<bool> DeleteSector(int idSector);
    }
}
