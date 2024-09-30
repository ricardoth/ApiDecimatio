namespace Decimatio.Domain.Interfaces
{
    public interface IMercadoPagoRepository
    {
        Task<IEnumerable<PreferenceTicket>> GetAll();
    }
}
