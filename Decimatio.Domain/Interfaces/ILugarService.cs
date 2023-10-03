namespace Decimatio.Domain.Interfaces
{
    public interface ILugarService
    {
        Task<IEnumerable<Lugar>> GetAllLugares(); 
    }
}
