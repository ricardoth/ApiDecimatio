namespace Decimatio.Domain.Interfaces
{
    public interface ISectorRepository
    {
        Task<IEnumerable<Sector>> GetAllSectores();
    }
}
