namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IMedioPagoRepository
    {
        Task<IEnumerable<MedioPago>> GetMedioPagos();
        Task<MedioPago> GetMedioPago(int id);
        Task AddMedioPago(MedioPago medioPago);
        Task<bool> DeleteMedioPago(int id);
        Task<bool> UpdateMedioPago(MedioPago medioPago);
    }
}
