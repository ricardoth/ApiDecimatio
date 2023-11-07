namespace Decimatio.Domain.Interfaces
{
    public interface ILugarRepository
    {
        Task<IEnumerable<Lugar>> GetAllLugares();
        Task<Lugar> GetById(int idLugar);
        Task<int> AddLugar(Lugar lugar);
        Task<bool> UpdateLugar(Lugar lugar);
        Task<bool> DeleteLugar(int idLugar);
    }
}
