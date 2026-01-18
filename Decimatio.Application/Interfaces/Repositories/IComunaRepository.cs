namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IComunaRepository
    {
        Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion);
    }
}
