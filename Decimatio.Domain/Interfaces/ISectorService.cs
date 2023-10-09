namespace Decimatio.Domain.Interfaces
{
    public interface ISectorService 
    {
        Task<IEnumerable<Sector>> GetAllSectores();
        Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento);
        Task<Sector> GetById(int idSector);
        Task AddSector(Sector sector);
    }
}
