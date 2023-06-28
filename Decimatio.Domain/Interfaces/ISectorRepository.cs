namespace Decimatio.Domain.Interfaces
{
    public interface ISectorRepository
    {
        Task<IEnumerable<Sector>> GetAllSectores();
        Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento);
        Task<Sector> GetById(int idSector);
    }
}
