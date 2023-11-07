namespace Decimatio.Domain.Interfaces
{
    public interface ILugarService
    {
        Task<IEnumerable<Lugar>> GetAllLugares();
        Task<Lugar> GetById(int idLugar);
        Task AddLugar(Lugar lugar);
        Task<bool> UpdateLugar(Lugar lugar);
        Task<bool> DeleteLugar(int idLugar);
    }
}
