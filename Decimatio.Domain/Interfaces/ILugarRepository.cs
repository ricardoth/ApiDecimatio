namespace Decimatio.Domain.Interfaces
{
    public interface ILugarRepository
    {
        Task<IEnumerable<Lugar>> GetAllLugares();
    }
}
