namespace Decimatio.Application.Interfaces.Services
{
    public interface IMedioPagoService
    {
        Task<IEnumerable<MedioPagoDto>> GetMediosPagosAsync();
        Task<MedioPagoDto> GetMedioPagoAsync(int id);
        Task AddMedioPagoAsync(MedioPagoDto medioPagoDto);
        Task<bool> DeleteMedioPagoAsync(int id);
        Task<bool> UpdateMedioPagoAsync(MedioPagoDto medioPagoDto);
    }
}
