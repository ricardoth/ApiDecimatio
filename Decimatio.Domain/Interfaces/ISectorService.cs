namespace Decimatio.Domain.Interfaces
{
    public interface ISectorService 
    {
        Task<IEnumerable<Sector>> GetAllSectores();
    }
}
