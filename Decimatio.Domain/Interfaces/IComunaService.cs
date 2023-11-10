namespace Decimatio.Domain.Interfaces
{
    public interface IComunaService
    {
        Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion);
    }
}
