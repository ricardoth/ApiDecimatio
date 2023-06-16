namespace Decimatio.Domain.Interfaces
{
    public interface IMedioPagoRepository
    {
        Task<IEnumerable<MedioPago>> GetMedioPagos();
        Task<MedioPago> GetMedioPago(int id);
        Task AddMedioPago(MedioPago medioPago);
        Task<int> DeleteMedioPago(int id);
        Task UpdateMedioPago(int id, MedioPago medioPago);
    }
}
