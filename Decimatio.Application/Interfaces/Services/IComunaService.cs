namespace Decimatio.Application.Interfaces.Services
{
    public interface IComunaService
    {
        Task<IEnumerable<Comuna>> GetComunasByRegion(int idRegion);
    }
}
