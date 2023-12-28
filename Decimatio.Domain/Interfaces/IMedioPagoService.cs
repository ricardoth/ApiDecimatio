namespace Decimatio.Domain.Interfaces
{
    public interface IMedioPagoService
    {
        Task<IEnumerable<MedioPago>> GetMediosPagosAsync();
        Task<MedioPago> GetMedioPagoAsync(int id);
        Task AddMedioPagoAsync(MedioPago medioPago);
        Task<bool> DeleteMedioPagoAsync(int id);
        Task<bool> UpdateMedioPagoAsync(MedioPago medioPago);
    }
}
