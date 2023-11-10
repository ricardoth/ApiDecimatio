namespace Decimatio.Domain.Interfaces
{
    public interface IComunaRepository
    {
        Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion);
    }
}
