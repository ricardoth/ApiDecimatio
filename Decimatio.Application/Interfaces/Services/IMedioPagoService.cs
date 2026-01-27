namespace Decimatio.Application.Interfaces.Services
{
    public interface IMedioPagoService
    {
        Task<IEnumerable<MedioPagoDto>> GetMediosPagosAsync();
        Task<MedioPagoDto> GetMedioPagoAsync(int id);
        Task AddMedioPagoAsync(CreateMedioPagoDto createMedioPagoDto);
        Task<bool> DeleteMedioPagoAsync(int id);
        Task<bool> UpdateMedioPagoAsync(UpdateMedioPagoDto medioPagoDto);
    }
}
