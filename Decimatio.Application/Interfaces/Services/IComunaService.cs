namespace Decimatio.Application.Interfaces.Services
{
    public interface IComunaService
    {
        Task<IEnumerable<ComunaDto>> GetComunasByRegion(int idRegion);
    }
}
