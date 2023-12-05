namespace Decimatio.Domain.Interfaces
{
    public interface IMedioPagoService
    {
        Task<IEnumerable<MedioPago>> GetMediosPagosAsync();
        Task<MedioPago> GetMedioPagoAsync(int id);
        Task AddMedioPagoAsync(MedioPago medioPago);
        Task<int> DeleteMedioPagoAsync(int id);
        Task UpdateMedioPagoAsync(int id, MedioPago medioPago);
    }
}
